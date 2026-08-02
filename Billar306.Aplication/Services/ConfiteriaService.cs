// ConfiteriaService.cs
using Billar306.Aplicacion.DTOs.Confiteria;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Clientes;
using Billar306.Dominio.Models.Venta;

namespace Billar306.Aplicacion.Services
{
    public class ConfiteriaService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IVentaConfiteriaRepository _ventaRepository;
        private readonly ICuentaBaseRepository _cuentaRepository;
        private readonly ISesionMesaRepository _sesionMesaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly ITurnoRepository _turnoRepository;
        private readonly IRegistroTurnoEmpleadoRepository _registroRepository;
        private readonly IItemConfiteriaRepository _itemRepository;

        public ConfiteriaService(
            IProductoRepository productoRepository,
            IVentaConfiteriaRepository ventaRepository,
            ICuentaBaseRepository cuentaRepository,
            ISesionMesaRepository sesionMesaRepository,
            IClienteRepository clienteRepository,
            ITurnoRepository turnoRepository,
            IRegistroTurnoEmpleadoRepository registroRepository,
            IItemConfiteriaRepository itemRepository)
        {
            _productoRepository = productoRepository;
            _ventaRepository = ventaRepository;
            _cuentaRepository = cuentaRepository;
            _sesionMesaRepository = sesionMesaRepository;
            _clienteRepository = clienteRepository;
            _turnoRepository = turnoRepository;
            _registroRepository = registroRepository;
            _itemRepository = itemRepository;
        }

        // ---------- Venta directa (sin mesa) ----------

        public async Task<(bool Exito, string? Error, VentaConfiteriaDto? Venta)> CrearVentaDirectaAsync(CrearVentaDirectaDto dto, int empleadoId)
        {
            var tieneClienteExistente = dto.ClienteId is not null;
            var tieneClienteNuevo = !string.IsNullOrWhiteSpace(dto.NombreClienteNuevo);
            if (tieneClienteExistente == tieneClienteNuevo)
                return (false, "Debe indicar exactamente uno: un cliente existente o el nombre de un cliente nuevo.", null);

            int clienteIdFinal;
            if (tieneClienteExistente)
            {
                var cliente = await _clienteRepository.ObtenerPorIdAsync(dto.ClienteId!.Value);
                if (cliente is null) return (false, "El cliente indicado no existe.", null);
                clienteIdFinal = cliente.Id;
            }
            else
            {
                var existente = await _clienteRepository.BuscarExactoAsync(dto.NombreClienteNuevo!);
                if (existente is not null)
                {
                    clienteIdFinal = existente.Id;
                }
                else
                {
                    var nuevoCliente = new Cliente { NombreCompleto = dto.NombreClienteNuevo! };
                    await _clienteRepository.AgregarAsync(nuevoCliente);
                    clienteIdFinal = nuevoCliente.Id;
                }
            }

            var turno = await _turnoRepository.ObtenerTurnoAbiertoAsync();
            if (turno is null) return (false, "No hay un turno abierto.", null);

            if (await _registroRepository.ObtenerAbiertoAsync(turno.Id, empleadoId) is null)
                return (false, "No estás activo en el turno actual.", null);

            var (items, total, error) = await ArmarItemsAsync(dto.Items);
            if (error is not null) return (false, error, null);

            var venta = new VentaConfiteria { Total = total };
            await _ventaRepository.AgregarAsync(venta);

            foreach (var item in items)
            {
                item.VentaConfiteriaId = venta.Id;
                await _itemRepository.AgregarAsync(item);
            }

            var cuenta = new CuentaBase
            {
                ClienteId = clienteIdFinal,
                TurnoId = turno.Id,
                EmpleadoAperturaId = empleadoId,
                VentaConfiteriaId = venta.Id,
                Total = venta.Total
            };
            await _cuentaRepository.AgregarAsync(cuenta);

            return (true, null, MapearADto(venta, items));
        }
        public async Task<(bool Exito, string? Error)> QuitarItemAsync(int itemId)
        {
            var item = await _itemRepository.ObtenerPorIdAsync(itemId);
            if (item is null) return (false, "Ítem no encontrado.");

            if (DateTime.UtcNow > item.FechaInicio.AddMinutes(1))
                return (false, "Ya pasó el minuto permitido para retirar este ítem.");

            var venta = await _ventaRepository.ObtenerPorIdAsync(item.VentaConfiteriaId);
            if (venta is null) return (false, "Venta asociada no encontrada.");

            venta.Total -= item.Total;
            await _ventaRepository.ActualizarAsync(venta);

            var cuenta = await _cuentaRepository.ObtenerPorVentaConfiteriaIdAsync(venta.Id);
            if (cuenta is not null)
            {
                cuenta.Total -= item.Total;
                await _cuentaRepository.ActualizarAsync(cuenta);
            }

            await _itemRepository.EliminarAsync(item); 

            return (true, null);
        }

        // ---------- Agregar consumición a una mesa ya abierta ----------

        public async Task<(bool Exito, string? Error, bool NoEncontrado, VentaConfiteriaDto? Venta)> AgregarAMesaAsync(int sesionMesaId, AgregarConsumicionMesaDto dto, int empleadoId)
        {
            var sesion = await _sesionMesaRepository.ObtenerPorIdAsync(sesionMesaId);
            if (sesion is null) return (false, "Sesión de mesa no encontrada.", true, null);
            if (sesion.FechaFin is not null) return (false, "La mesa ya está cerrada, no se pueden agregar consumiciones.", false, null);

            var turnoActual = await _turnoRepository.ObtenerTurnoAbiertoAsync();
            if (turnoActual is null) return (false, "No hay un turno abierto.", false, null);

            if (await _registroRepository.ObtenerAbiertoAsync(turnoActual.Id, empleadoId) is null)
                return (false, "No estás activo en el turno actual.", false, null);

            var (nuevosItems, totalNuevo, error) = await ArmarItemsAsync(dto.Items);
            if (error is not null) return (false, error, false, null);

            VentaConfiteria venta;
            if (sesion.VentaConfiteriaId is null)
            {
                venta = new VentaConfiteria { Total = totalNuevo };
                await _ventaRepository.AgregarAsync(venta);
                sesion.VentaConfiteriaId = venta.Id;
            }
            else
            {
                venta = (await _ventaRepository.ObtenerConItemsAsync(sesion.VentaConfiteriaId.Value))!;
                venta.Total += totalNuevo;
                await _ventaRepository.ActualizarAsync(venta);
            }

            foreach (var item in nuevosItems)
            {
                item.VentaConfiteriaId = venta.Id;
                await _itemRepository.AgregarAsync(item);
            }

            sesion.Total = sesion.MontoSesionMesa + venta.Total;
            await _sesionMesaRepository.ActualizarAsync(sesion);

            var ventaCompleta = await _ventaRepository.ObtenerConItemsAsync(venta.Id);
            return (true, null, false, MapearADto(ventaCompleta!, ventaCompleta!.ItemsConfiterias.ToList()));
        }

        // ---------- Helpers privados ----------

        private async Task<(List<ItemConfiteria> Items, decimal Total, string? Error)> ArmarItemsAsync(List<ItemVentaDto> pedido)
        {
            if (pedido is null || pedido.Count == 0)
                return (new List<ItemConfiteria>(), 0, "Debe indicar al menos un producto.");

            var items = new List<ItemConfiteria>();
            decimal total = 0;

            foreach (var linea in pedido)
            {
                var producto = await _productoRepository.ObtenerPorIdAsync(linea.ProductoId);
                if (producto is null)
                    return (items, total, $"El producto con Id {linea.ProductoId} no existe.");

                var totalLinea = producto.Precio * linea.Cantidad;
                items.Add(new ItemConfiteria
                {
                    ProductoId = producto.Id,
                    Nombre = producto.Nombre,
                    Cantidad = linea.Cantidad,
                    PrecioUnitario = producto.Precio,
                    Total = totalLinea
                });
                total += totalLinea;
            }

            return (items, total, null);
        }

        public async Task<VentaConfiteriaDto?> ObtenerVentaAsync(int ventaId)
        {
            var venta = await _ventaRepository.ObtenerConItemsAsync(ventaId);
            if (venta is null) return null;

            return new VentaConfiteriaDto(
                venta.Id, venta.Total,
                venta.ItemsConfiterias
                    .Select(i => new ItemConfiteriaDto(i.Id, i.ProductoId, i.Nombre, i.Cantidad, i.PrecioUnitario, i.Total, i.FechaInicio))
                    .ToList()
            );
        }
        public async Task<List<VentaConfiteriaDto>> ObtenerTodasAsync()
        {
            var ventas = await _ventaRepository.ObtenerTodasConItemsAsync();
            return ventas.Select(v => new VentaConfiteriaDto(
                v.Id, v.Total,
                v.ItemsConfiterias.Select(i => new ItemConfiteriaDto(i.Id, i.ProductoId, i.Nombre, i.Cantidad, i.PrecioUnitario, i.Total, i.FechaInicio)).ToList()
            )).ToList();
        }

        private readonly Dictionary<int, IRepository<ItemConfiteria>> _cacheNoUsado = new(); 

        private static VentaConfiteriaDto MapearADto(VentaConfiteria v, List<ItemConfiteria> items)
            => new(v.Id, v.Total,
                items.Select(i => new ItemConfiteriaDto(i.Id, i.ProductoId, i.Nombre, i.Cantidad, i.PrecioUnitario, i.Total, i.FechaInicio)).ToList());
    }
}
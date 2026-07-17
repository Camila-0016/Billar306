using Billar306.API.Repositories;
using Billar306.Data.Models.Venta;

namespace Billar306.API.Services
{
    public class ConfiteriaService
    {
        private readonly IItemConfiteriaRepository _itemRepo;
        private readonly ISesionMesaRepository _sesionRepo;
        private readonly IVentaDirectaRepository _ventaDirectaRepo;


        public ConfiteriaService(IItemConfiteriaRepository itemRepo, ISesionMesaRepository sesionRepo, IVentaDirectaRepository ventaDirectaRepo)
        {
            _itemRepo = itemRepo;
            _sesionRepo = sesionRepo;
            _ventaDirectaRepo = ventaDirectaRepo;
        }

        public async Task<List<ItemConfiteria>> ObtenerTodosAsync()
            => await _itemRepo.ObtenerTodosActivosAsync();

        public async Task<(bool ok, string error)> CrearItemAsync(
            string nombre, decimal precio, int stockInicial, int stockMinimo)
        {
            var item = new ItemConfiteria
            {
                Nombre = nombre,
                Precio = precio,
                StockActual = stockInicial,
                StockMinimo = stockMinimo,
                Activo = true
            };

            await _itemRepo.AgregarAsync(item);
            await _itemRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }

        public async Task<(bool ok, string error, bool stockBajo)> AgregarItemAMesaAsync(int mesaId, int itemId)
        {
            var item = await _itemRepo.ObtenerPorIdAsync(itemId);
            if (item == null) return (false, "Ítem no encontrado.", false);

            if (item.StockActual <= 0)
                return (false, "Sin stock. No se puede agregar este ítem.", false);

            var sesion = await _sesionRepo.ObtenerSesionAbiertaPorMesaAsync(mesaId);
            if (sesion == null) return (false, "No hay sesión abierta en esa mesa.", false);

            sesion.Consumiciones.Add(new ConsumicionMesa
            {
                SesionMesaId = sesion.Id,
                ItemConfiteriaId = itemId,
                Cantidad = 1,
                PrecioUnitario = item.Precio
            });

            item.StockActual--;
            bool stockBajo = item.StockActual <= item.StockMinimo;

            await _sesionRepo.GuardarCambiosAsync();
            return (true, string.Empty, stockBajo);
        }

        public async Task<(bool ok, string error)> ActualizarPrecioAsync(int itemId, decimal nuevoPrecio)
        {
            var item = await _itemRepo.ObtenerPorIdAsync(itemId);
            if (item == null) return (false, "Ítem no encontrado.");

            item.Precio = nuevoPrecio;
            await _itemRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }

        public async Task<(bool ok, string error)> ActualizarStockAsync(int itemId, int nuevoStock)
        {
            var item = await _itemRepo.ObtenerPorIdAsync(itemId);
            if (item == null) return (false, "Ítem no encontrado.");

            item.StockActual = nuevoStock;
            await _itemRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }

        public async Task<(bool ok, string error)> VenderDirectoAsync(
    int turnoId, int usuarioId, int itemId, int cantidad)
        {
            var item = await _itemRepo.ObtenerPorIdAsync(itemId);
            if (item == null) return (false, "Ítem no encontrado.");
            if (item.StockActual < cantidad)
                return (false, $"Stock insuficiente. Stock actual: {item.StockActual}.");

            item.StockActual -= cantidad;

            var venta = new VentaDirecta
            {
                TurnoId = turnoId,
                UsuarioId = usuarioId,
                ItemConfiteriaId = itemId,
                Cantidad = cantidad,
                PrecioUnitario = item.Precio,
                Total = item.Precio * cantidad,
                Timestamp = DateTime.Now
            };

            await _ventaDirectaRepo.AgregarAsync(venta);
            await _ventaDirectaRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }

        public async Task<List<VentaDirecta>> ObtenerVentasDelTurnoAsync(int turnoId)
            => await _ventaDirectaRepo.ObtenerPorTurnoAsync(turnoId);
    }
}
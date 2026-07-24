using Billar306.Aplicacion.DTOs.Mesas;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Control;
using Billar306.Dominio.Models.Venta.Mesas;
using Billar306.Dominio.Models.Clientes;

namespace Billar306.Aplicacion.Services
{
    public class SesionMesaService
    {
        private readonly ISesionMesaRepository _sesionRepository;
        private readonly IMesaRepository _mesaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly ITurnoRepository _turnoRepository;
        private readonly IRegistroTurnoEmpleadoRepository _registroRepository;
        private readonly IConfiguracionSistemaRepository _configRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public SesionMesaService(
            ISesionMesaRepository sesionRepository,
            IMesaRepository mesaRepository,
            IClienteRepository clienteRepository,
            ITurnoRepository turnoRepository,
            IRegistroTurnoEmpleadoRepository registroRepository,
            IConfiguracionSistemaRepository configRepository,
            IUsuarioRepository usuarioRepository)
        {
            _sesionRepository = sesionRepository;
            _mesaRepository = mesaRepository;
            _clienteRepository = clienteRepository;
            _turnoRepository = turnoRepository;
            _registroRepository = registroRepository;
            _configRepository = configRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<(bool Exito, string? Error, bool NoEncontrado, SesionMesaDto? Sesion)> AbrirAsync(AbrirSesionMesaDto dto)
        {
            var mesa = await _mesaRepository.ObtenerPorIdAsync(dto.MesaId);
            if (mesa is null) return (false, "Mesa no encontrada.", true, null);
            if (mesa.Ocupada) return (false, "La mesa ya está ocupada.", false, null);

            // Lógica para determinar si se usa un cliente existente o se crea uno nuevo
            var tieneClienteExistente = dto.ClienteId is not null;
            var tieneClienteNuevo = !string.IsNullOrWhiteSpace(dto.NombreClienteNuevo);

            if (tieneClienteExistente == tieneClienteNuevo)
                return (false, "Debe indicar exactamente uno: un cliente existente o el nombre de un cliente nuevo.", false, null);

            int clienteIdFinal;
            if (tieneClienteExistente)
            {
                var cliente = await _clienteRepository.ObtenerPorIdAsync(dto.ClienteId!.Value);
                if (cliente is null) return (false, "El cliente indicado no existe.", false, null);
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
            if (turno is null)
                return (false, "No hay un turno abierto.", false, null);

            if (await _registroRepository.ObtenerAbiertoAsync(turno.Id, dto.EmpleadoAperturaId) is null)
                return (false, "El empleado indicado no está activo en el turno actual.", false, null);

            // Apertura de la sesión
            var sesion = new SesionMesa
            {
                MesaId = dto.MesaId,
                ClienteId = clienteIdFinal, 
                TurnoId = turno.Id,
                EmpleadoAperturaId = dto.EmpleadoAperturaId,
                Total = 0,
                MontoSesionMesa = 0
            };
            await _sesionRepository.AgregarAsync(sesion);

            // Actualización del estado de la mesa
            mesa.Ocupada = true;
            await _mesaRepository.ActualizarAsync(mesa);

            return (true, null, false, await MapearADtoAsync(sesion));
        }

        public async Task<(bool Exito, string? Error, bool NoEncontrado)> CerrarAsync(int sesionId, CerrarSesionMesaDto dto)
        {
            var sesion = await _sesionRepository.ObtenerPorIdAsync(sesionId);
            if (sesion is null) return (false, "Sesión no encontrada.", true);
            if (sesion.FechaFin is not null) return (false, "La sesión ya está cerrada.", false);

            var turnoActual = await _turnoRepository.ObtenerTurnoAbiertoAsync();
            if (turnoActual is null) return (false, "No hay un turno abierto.", false);

            if (await _registroRepository.ObtenerAbiertoAsync(turnoActual.Id, dto.EmpleadoCierreId) is null)
                return (false, "El empleado indicado no está activo en el turno actual.", false);

            var tarifa = await _configRepository.ObtenerPorClaveAsync(TipoParametro.TarifaHoraMesa);
            if (tarifa is null)
                return (false, "No hay tarifa de hora de mesa configurada. El Jefe debe configurarla primero.", false);

            var ahora = DateTime.UtcNow;
            var horas = (decimal)(ahora - sesion.FechaInicio).TotalHours;
            var monto = Math.Round(horas * tarifa.Valor, 2);

            sesion.FechaFin = ahora;
            sesion.MontoSesionMesa = monto;
            sesion.Total = monto; 
            sesion.EmpleadoCierreId = dto.EmpleadoCierreId;
            await _sesionRepository.ActualizarAsync(sesion);

            var mesa = await _mesaRepository.ObtenerPorIdAsync(sesion.MesaId);
            if (mesa is not null)
            {
                mesa.Ocupada = false;
                await _mesaRepository.ActualizarAsync(mesa);
            }

            return (true, null, false);
        }

        private async Task<SesionMesaDto> MapearADtoAsync(SesionMesa s)
        {
            decimal montoMesaActual;

            if (s.FechaFin is not null)
            {
                montoMesaActual = s.MontoSesionMesa; 
            }
            else
            {
                var tarifa = await _configRepository.ObtenerPorClaveAsync(TipoParametro.TarifaHoraMesa);
                var horas = (decimal)(DateTime.UtcNow - s.FechaInicio).TotalHours;
                montoMesaActual = tarifa is null ? 0 : Math.Round(horas * tarifa.Valor, 2);
            }

            var confiteriaTotal = s.Total - s.MontoSesionMesa; 
            var totalActual = montoMesaActual + confiteriaTotal;

            return new SesionMesaDto(
                s.Id, s.MesaId, s.ClienteId, s.TurnoId, s.EmpleadoAperturaId, s.EmpleadoCierreId,
                s.FechaInicio, s.FechaFin, s.MontoSesionMesa, s.Total,
                montoMesaActual, totalActual, s.VentaConfiteriaId
            );
        }

        public async Task<List<SesionMesaDto>> ObtenerTodasAsync()
        {
            var sesiones = await _sesionRepository.ObtenerTodosAsync();
            var resultado = new List<SesionMesaDto>();
            foreach (var s in sesiones) resultado.Add(await MapearADtoAsync(s));
            return resultado;
        }

        public async Task<List<SesionMesaDto>> ObtenerAbiertasAsync()
        {
            var sesiones = await _sesionRepository.ObtenerAbiertasAsync();
            var resultado = new List<SesionMesaDto>();
            foreach (var s in sesiones) resultado.Add(await MapearADtoAsync(s));
            return resultado;
        }

        public async Task<SesionMesaDto?> ObtenerPorIdAsync(int id)
        {
            var sesion = await _sesionRepository.ObtenerPorIdAsync(id);
            return sesion is null ? null : await MapearADtoAsync(sesion);
        }


    }
}
// ReportesService.cs
using Billar306.Aplicacion.DTOs.Reportes;
using Billar306.Aplicacion.DTOs.Turnos;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Venta.Mesas;

namespace Billar306.Aplicacion.Services
{
    public class ReportesService
    {
        private readonly ITurnoRepository _turnoRepository;
        private readonly IRegistroTurnoEmpleadoRepository _registroRepository;
        private readonly ICuentaBaseRepository _cuentaRepository;
        private readonly IMesaRepository _mesaRepository;
        private readonly IClienteRepository _clienteRepository;

        public ReportesService(
            ITurnoRepository turnoRepository,
            IRegistroTurnoEmpleadoRepository registroRepository,
            ICuentaBaseRepository cuentaRepository,
            IMesaRepository mesaRepository,
            IClienteRepository clienteRepository)
        {
            _turnoRepository = turnoRepository;
            _registroRepository = registroRepository;
            _cuentaRepository = cuentaRepository;
            _mesaRepository = mesaRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<TurnoReporteDto?> ObtenerDetalleTurnoAsync(int turnoId)
        {
            var turno = await _turnoRepository.ObtenerPorIdAsync(turnoId);
            if (turno is null) return null;

            var registros = await _registroRepository.ObtenerPorTurnoAsync(turnoId);
            var horasDto = registros
                .Select(r => new RegistroTurnoEmpleadoDto(r.Id, r.TurnoId, r.EmpleadoId, r.FechaInicio, r.Salida, r.HorasTrabajadas))
                .ToList();

            var cuentas = await _cuentaRepository.ObtenerPorTurnoAsync(turnoId);

            var mesas = new List<MesaDelTurnoDto>();
            foreach (var sesion in cuentas.OfType<SesionMesa>())
            {
                var mesaFisica = await _mesaRepository.ObtenerPorIdAsync(sesion.MesaId);
                var cliente = await _clienteRepository.ObtenerPorIdAsync(sesion.ClienteId);
                mesas.Add(new MesaDelTurnoDto(
                    sesion.Id, mesaFisica?.Numero ?? 0, cliente?.NombreCompleto ?? "—",
                    sesion.Total, sesion.FechaFin is not null));
            }

            var ventasDirectas = new List<VentaDirectaDelTurnoDto>();
            foreach (var cuenta in cuentas.Where(c => c is not SesionMesa))
            {
                var cliente = await _clienteRepository.ObtenerPorIdAsync(cuenta.ClienteId);
                ventasDirectas.Add(new VentaDirectaDelTurnoDto(cuenta.Id, cliente?.NombreCompleto ?? "—", cuenta.Total));
            }

            return new TurnoReporteDto(
                turno.Id, turno.TitularId, turno.AuxiliarId, turno.FechaInicio, turno.Salida,
                horasDto, mesas, ventasDirectas);
        }
    }
}
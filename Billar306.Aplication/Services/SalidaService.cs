// SalidaService.cs
using Billar306.Aplicacion.DTOs.Salida;
using Billar306.Dominio.Interfaces;

namespace Billar306.Aplicacion.Services
{
    public class SalidaService
    {
        private readonly ITurnoRepository _turnoRepository;
        private readonly IRegistroTurnoEmpleadoRepository _registroRepository;
        private readonly ISesionMesaRepository _sesionMesaRepository;
        private readonly IDiaLaboralRepository _diaLaboralRepository;

        public SalidaService(
            ITurnoRepository turnoRepository,
            IRegistroTurnoEmpleadoRepository registroRepository,
            ISesionMesaRepository sesionMesaRepository,
            IDiaLaboralRepository diaLaboralRepository)
        {
            _turnoRepository = turnoRepository;
            _registroRepository = registroRepository;
            _sesionMesaRepository = sesionMesaRepository;
            _diaLaboralRepository = diaLaboralRepository;
        }

        public async Task<(bool Exito, string? Error, EstadoSalidaDto? Estado)> ObtenerEstadoAsync(int empleadoId)
        {
            var turno = await _turnoRepository.ObtenerTurnoAbiertoAsync();
            if (turno is null) return (false, "No hay un turno abierto.", null);

            var miRegistro = await _registroRepository.ObtenerAbiertoAsync(turno.Id, empleadoId);
            if (miRegistro is null) return (false, "No estás activo en el turno actual.", null);

            var activos = (await _registroRepository.ObtenerAbiertosPorTurnoAsync(turno.Id)).ToList();
            var esUnico = activos.Count == 1;

            var mesasAbiertas = await _sesionMesaRepository.ObtenerAbiertasAsync();

            return (true, null, new EstadoSalidaDto(turno.Id, esUnico, mesasAbiertas.Any()));
        }

        public async Task<(bool Exito, string? Error, string? Aviso)> ConfirmarSalidaAsync(ConfirmarSalidaDto dto)
        {
            var turno = await _turnoRepository.ObtenerTurnoAbiertoAsync();
            if (turno is null) return (false, "No hay un turno abierto.", null);

            var miRegistro = await _registroRepository.ObtenerAbiertoAsync(turno.Id, dto.EmpleadoId);
            if (miRegistro is null) return (false, "No estás activo en el turno actual.", null);

            var activos = (await _registroRepository.ObtenerAbiertosPorTurnoAsync(turno.Id)).ToList();
            var esUnico = activos.Count == 1;
            var ahora = DateTime.UtcNow;

            if (!esUnico)
            {
                miRegistro.Salida = ahora;
                miRegistro.HorasTrabajadas = Math.Round((decimal)(ahora - miRegistro.FechaInicio).TotalHours, 2);
                await _registroRepository.ActualizarAsync(miRegistro);
                return (true, null, null);
            }

            // Es el único activo: se cierra el turno completo
            turno.Salida = ahora;
            await _turnoRepository.ActualizarAsync(turno);

            miRegistro.Salida = ahora;
            miRegistro.HorasTrabajadas = Math.Round((decimal)(ahora - miRegistro.FechaInicio).TotalHours, 2);
            await _registroRepository.ActualizarAsync(miRegistro);

            string? aviso = null;
            if (dto.CerrarDiaLaboral)
            {
                var mesasAbiertas = await _sesionMesaRepository.ObtenerAbiertasAsync();
                if (mesasAbiertas.Any())
                {
                    aviso = "El turno se cerró. El día laboral no se cerró porque hay mesas abiertas.";
                }
                else
                {
                    var dia = await _diaLaboralRepository.ObtenerAbiertoActualAsync();
                    if (dia is not null)
                    {
                        dia.EstaCerrado = true;
                        dia.FechaCierre = ahora;
                        await _diaLaboralRepository.ActualizarAsync(dia);
                    }
                }
            }

            return (true, null, aviso);
        }
    }
}
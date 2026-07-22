
using Billar306.Aplicacion.DTOs.Turnos;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Operatividad;

namespace Billar306.Aplicacion.Services
{
    public class DiaLaboralService
    {
        private readonly IDiaLaboralRepository _diaLaboralRepository;
        private readonly ITurnoRepository _turnoRepository;
        private readonly IRegistroTurnoEmpleadoRepository _registroRepository;

        public DiaLaboralService(
            IDiaLaboralRepository diaLaboralRepository,
            ITurnoRepository turnoRepository,
            IRegistroTurnoEmpleadoRepository registroRepository)
        {
            _diaLaboralRepository = diaLaboralRepository;
            _turnoRepository = turnoRepository;
            _registroRepository = registroRepository;
        }

        public async Task<(bool Exito, string? Error, DiaLaboralDto? Dia)> AbrirAsync()
        {
            if (await _diaLaboralRepository.ObtenerAbiertoActualAsync() is not null)
                return (false, "Ya hay un día laboral abierto.", null);

            var dia = new DiaLaboral();
            await _diaLaboralRepository.AgregarAsync(dia);

            return (true, null, MapearADto(dia));
        }

        public async Task<(bool Exito, string? Error)> CerrarAsync(int diaLaboralId)
        {
            var dia = await _diaLaboralRepository.ObtenerPorIdAsync(diaLaboralId);
            if (dia is null) return (false, "Día laboral no encontrado.");
            if (dia.EstaCerrado) return (false, "El día laboral ya está cerrado.");

            if (await _turnoRepository.ObtenerTurnoAbiertoAsync() is not null)
                return (false, "No se puede cerrar el día laboral mientras haya un turno abierto.");

            dia.EstaCerrado = true;
            dia.FechaCierre = DateTime.UtcNow;
            await _diaLaboralRepository.ActualizarAsync(dia);

            return (true, null);
        }

        public async Task<List<DiaLaboralDto>> ObtenerTodosAsync()
        {
            var dias = await _diaLaboralRepository.ObtenerTodosAsync();
            return dias.Select(MapearADto).ToList();
        }

        public async Task<DiaLaboralDetalleDto?> ObtenerDetalleAsync(int id)
        {
            var dia = await _diaLaboralRepository.ObtenerConTurnosAsync(id);
            if (dia is null) return null;

            var turnosDetalle = new List<TurnoDetalleDto>();
            foreach (var turno in dia.Turnos)
            {
                var registros = await _registroRepository.ObtenerPorTurnoAsync(turno.Id);
                var registrosDto = registros
                    .Select(r => new RegistroTurnoEmpleadoDto(r.Id, r.TurnoId, r.EmpleadoId, r.FechaInicio, r.Salida, r.HorasTrabajadas))
                    .ToList();

                turnosDetalle.Add(new TurnoDetalleDto(
                    turno.Id, turno.TitularId, turno.AuxiliarId, turno.FechaInicio, turno.Salida, registrosDto));
            }

            return new DiaLaboralDetalleDto(dia.Id, dia.FechaInicio, dia.FechaCierre, dia.EstaCerrado, turnosDetalle);
        }

        private static DiaLaboralDto MapearADto(DiaLaboral d)
            => new(d.Id, d.FechaInicio, d.FechaCierre, d.EstaCerrado);
    }
}
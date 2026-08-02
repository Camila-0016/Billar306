using Billar306.Aplicacion.DTOs.Turnos;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Empleado;
using Billar306.Dominio.Models.Operatividad;

namespace Billar306.Aplicacion.Services
{
    public class TurnoService
    {
        private readonly ITurnoRepository _turnoRepository;
        private readonly IDiaLaboralRepository _diaLaboralRepository;
        private readonly IRegistroTurnoEmpleadoRepository _registroRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public TurnoService(
            ITurnoRepository turnoRepository,
            IDiaLaboralRepository diaLaboralRepository,
            IRegistroTurnoEmpleadoRepository registroRepository,
            IUsuarioRepository usuarioRepository)
        {
            _turnoRepository = turnoRepository;
            _diaLaboralRepository = diaLaboralRepository;
            _registroRepository = registroRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<(bool Exito, string? Error, TurnoDto? Turno)> AbrirAsync(int titularId)
        {
            if (await _turnoRepository.ObtenerTurnoAbiertoAsync() is not null)
                return (false, "Ya hay un turno abierto. Debe cerrarse antes de abrir uno nuevo.", null);

            var diaLaboral = await _diaLaboralRepository.ObtenerAbiertoActualAsync();
            if (diaLaboral is null)
                return (false, "No hay un día laboral abierto. Debe abrirse antes de iniciar un turno.", null);

            var turno = new Turno
            {
                DiaLaboralId = diaLaboral.Id,
                TitularId = titularId,
                TotalMaquinas = 0
            };
            await _turnoRepository.AgregarAsync(turno);

            await _registroRepository.AgregarAsync(new RegistroTurnoEmpleado
            {
                TurnoId = turno.Id,
                EmpleadoId = titularId
            });

            return (true, null, MapearADto(turno));
        }

        public async Task<(bool Exito, string? Error, bool NoEncontrado)> AsignarAuxiliarAsync(int turnoId, AsignarAuxiliarDto dto, int empleadoIdQueLlama)
        {
            var turno = await _turnoRepository.ObtenerPorIdAsync(turnoId);
            if (turno is null) return (false, "Turno no encontrado.", true);
            if (turno.Salida is not null) return (false, "El turno ya está cerrado.", false);

            if (turno.TitularId != empleadoIdQueLlama)
                return (false, "Solo el titular puede asignar auxiliares.", false);

            if (dto.AuxiliarId == turno.TitularId)
                return (false, "El auxiliar no puede ser la misma persona que el titular.", false);

            if (turno.AuxiliarId is not null)
            {
                var registroPrevio = await _registroRepository.ObtenerAbiertoAsync(turnoId, turno.AuxiliarId.Value);
                if (registroPrevio is not null)
                    return (false, "Ya hay un auxiliar activo en este turno.", false);
            }

            if (await _usuarioRepository.ObtenerPorIdAsync(dto.AuxiliarId) is null)
                return (false, "El auxiliar indicado no existe.", false);

            turno.AuxiliarId = dto.AuxiliarId;
            await _turnoRepository.ActualizarAsync(turno);

            await _registroRepository.AgregarAsync(new RegistroTurnoEmpleado
            {
                TurnoId = turnoId,
                EmpleadoId = dto.AuxiliarId
            });

            return (true, null, false);
        }

        public async Task<(bool Exito, string? Error, bool NoEncontrado)> RetirarAuxiliarAsync(int turnoId, int empleadoId)
        {
            var turno = await _turnoRepository.ObtenerPorIdAsync(turnoId);
            if (turno is null) return (false, "Turno no encontrado.", true);

            if (empleadoId == turno.TitularId)
                return (false, "No se puede retirar al titular por esta vía; use el cierre de turno.", false);

            var registro = await _registroRepository.ObtenerAbiertoAsync(turnoId, empleadoId);
            if (registro is null)
                return (false, "Este empleado no está activo en este turno.", false);

            var ahora = DateTime.UtcNow;
            registro.Salida = ahora;
            registro.HorasTrabajadas = CalcularHoras(registro.FechaInicio, ahora);
            await _registroRepository.ActualizarAsync(registro);

            return (true, null, false);
        }

        public async Task<(bool Exito, string? Error, bool NoEncontrado)> CerrarAsync(int turnoId)
        {
            var turno = await _turnoRepository.ObtenerPorIdAsync(turnoId);
            if (turno is null) return (false, "Turno no encontrado.", true);
            if (turno.Salida is not null) return (false, "El turno ya está cerrado.", false);

            var ahora = DateTime.UtcNow;
            turno.Salida = ahora;
            await _turnoRepository.ActualizarAsync(turno);

            var registrosAbiertos = await _registroRepository.ObtenerAbiertosPorTurnoAsync(turnoId);
            foreach (var registro in registrosAbiertos)
            {
                registro.Salida = ahora;
                registro.HorasTrabajadas = CalcularHoras(registro.FechaInicio, ahora);
                await _registroRepository.ActualizarAsync(registro);
            }

            return (true, null, false);
        }

        public async Task<List<TurnoDto>> ObtenerTodosAsync()
        {
            var turnos = await _turnoRepository.ObtenerTodosAsync();
            return turnos.Select(MapearADto).ToList();
        }

        public async Task<TurnoDto?> ObtenerPorIdAsync(int id)
        {
            var turno = await _turnoRepository.ObtenerPorIdAsync(id);
            return turno is null ? null : MapearADto(turno);
        }

        private static decimal CalcularHoras(DateTime inicio, DateTime fin)
            => Math.Round((decimal)(fin - inicio).TotalHours, 2);

        private static TurnoDto MapearADto(Turno t)
            => new(t.Id, t.DiaLaboralId, t.TitularId, t.AuxiliarId, t.FechaInicio, t.Salida);

        public async Task<List<RegistroTurnoEmpleadoDto>> ObtenerHorasPorEmpleadoAsync(int empleadoId, DateTime? desde, DateTime? hasta)
        {
            var registros = await _registroRepository.ObtenerPorEmpleadoAsync(empleadoId, desde, hasta);
            return registros
                .Select(r => new RegistroTurnoEmpleadoDto(r.Id, r.TurnoId, r.EmpleadoId, r.FechaInicio, r.Salida, r.HorasTrabajadas))
                .ToList();
        }
        public async Task<List<RegistroTurnoEmpleadoDto>?> ObtenerActivosAsync(int turnoId)
        {
            if (await _turnoRepository.ObtenerPorIdAsync(turnoId) is null) return null;

            var activos = await _registroRepository.ObtenerAbiertosPorTurnoAsync(turnoId);
            return activos
                .Select(r => new RegistroTurnoEmpleadoDto(r.Id, r.TurnoId, r.EmpleadoId, r.FechaInicio, r.Salida, r.HorasTrabajadas))
                .ToList();
        }
    }
}
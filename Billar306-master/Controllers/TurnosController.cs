using Billar306.API.Extensions;
using Billar306.Aplicacion.DTOs.Turnos;
using Billar306.Aplicacion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TurnosController : ControllerBase
    {
        private readonly TurnoService _turnoService;

        public TurnosController(TurnoService turnoService)
        {
            _turnoService = turnoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TurnoDto>>> ObtenerTodos()
            => Ok(await _turnoService.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<TurnoDto>> ObtenerPorId(int id)
        {
            var turno = await _turnoService.ObtenerPorIdAsync(id);
            if (turno is null) return NotFound(new { mensaje = "Turno no encontrado" });
            return Ok(turno);
        }

        [HttpPost("abrir")]
        public async Task<ActionResult<TurnoDto>> Abrir()
        {
            var (exito, error, turno) = await _turnoService.AbrirAsync(this.UsuarioIdActual());
            if (!exito) return Conflict(new { mensaje = error });
            return CreatedAtAction(nameof(ObtenerPorId), new { id = turno!.Id }, turno);
        }

        [HttpPost("{id}/auxiliar")]
        public async Task<IActionResult> AsignarAuxiliar(int id, [FromBody] AsignarAuxiliarDto dto)
        {
            var (exito, error, noEncontrado) = await _turnoService.AsignarAuxiliarAsync(id, dto, this.UsuarioIdActual());
            if (!exito)
                return noEncontrado ? NotFound(new { mensaje = error }) : Conflict(new { mensaje = error });
            return NoContent();
        }

        [HttpDelete("{id}/auxiliar/{empleadoId}")]
        public async Task<IActionResult> RetirarAuxiliar(int id, int empleadoId)
        {
            var (exito, error, noEncontrado) = await _turnoService.RetirarAuxiliarAsync(id, empleadoId);
            if (!exito)
                return noEncontrado
                    ? NotFound(new { mensaje = error })
                    : Conflict(new { mensaje = error });
            return NoContent();
        }

        [HttpPost("{id}/cerrar")]
        public async Task<IActionResult> Cerrar(int id)
        {
            var (exito, error, noEncontrado) = await _turnoService.CerrarAsync(id);
            if (!exito)
                return noEncontrado
                    ? NotFound(new { mensaje = error })
                    : Conflict(new { mensaje = error });
            return NoContent();
        }

        [HttpGet("horas-empleado/{empleadoId}")]
        public async Task<ActionResult<List<RegistroTurnoEmpleadoDto>>> ObtenerHorasPorEmpleado(
    int empleadoId, [FromQuery] DateTime? desde, [FromQuery] DateTime? hasta)
        {
            var registros = await _turnoService.ObtenerHorasPorEmpleadoAsync(empleadoId, desde, hasta);
            return Ok(registros);
        }

        [HttpGet("{id}/activos")]
        public async Task<ActionResult<List<RegistroTurnoEmpleadoDto>>> ObtenerActivos(int id)
        {
            var activos = await _turnoService.ObtenerActivosAsync(id);
            if (activos is null) return NotFound(new { mensaje = "Turno no encontrado" });
            return Ok(activos);
        }
    }
}
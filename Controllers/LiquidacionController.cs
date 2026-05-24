using Billar306.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Billar306.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LiquidacionController : ControllerBase
    {
        private readonly LiquidacionService _liquidacionService;
        private readonly TurnoService _turnoService;

        public LiquidacionController(LiquidacionService liquidacionService, TurnoService turnoService)
        {
            _liquidacionService = liquidacionService;
            _turnoService = turnoService;
        }

        [HttpPost("entrada")]
        public async Task<IActionResult> RegistrarEntrada()
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var turno = await _turnoService.ObtenerTurnoAbiertoAsync(usuarioId);
            if (turno == null)
                return BadRequest(new { mensaje = "No hay turno abierto." });

            var (ok, error) = await _liquidacionService.RegistrarEntradaAsync(usuarioId, turno.Id);
            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Entrada registrada." });
        }

        [HttpPost("salida")]
        public async Task<IActionResult> RegistrarSalida()
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var turno = await _turnoService.ObtenerTurnoAbiertoAsync(usuarioId);
            if (turno == null)
                return BadRequest(new { mensaje = "No hay turno abierto." });

            var (ok, error) = await _liquidacionService.RegistrarSalidaAsync(usuarioId, turno.Id);
            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Salida registrada." });
        }

        [HttpGet("empleado/{usuarioId}")]
        [Authorize(Roles = "encargado,jefe")]
        public async Task<IActionResult> ObtenerLiquidacion(
            int usuarioId,
            [FromQuery] DateTime desde,
            [FromQuery] DateTime hasta)
        {
            var resultado = await _liquidacionService.CalcularLiquidacionAsync(usuarioId, desde, hasta);
            return Ok(resultado);
        }

        [HttpGet("todos")]
        [Authorize(Roles = "encargado,jefe")]
        public async Task<IActionResult> ObtenerLiquidacionTodos(
            [FromQuery] DateTime desde,
            [FromQuery] DateTime hasta)
        {
            var resultado = await _liquidacionService.CalcularLiquidacionTodosAsync(desde, hasta);
            return Ok(resultado);
        }

        [HttpGet("mis-horas")]
        public async Task<IActionResult> ObtenerMisHoras(
            [FromQuery] DateTime desde,
            [FromQuery] DateTime hasta)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var resultado = await _liquidacionService.CalcularLiquidacionAsync(usuarioId, desde, hasta);
            return Ok(resultado);
        }
    }
}
using Billar306.Aplicacion.DTOs.Turnos;
using Billar306.Aplicacion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DiaLaboralesController : ControllerBase
    {
        private readonly DiaLaboralService _diaLaboralService;

        public DiaLaboralesController(DiaLaboralService diaLaboralService)
        {
            _diaLaboralService = diaLaboralService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DiaLaboralDto>>> ObtenerTodos()
            => Ok(await _diaLaboralService.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<DiaLaboralDetalleDto>> ObtenerDetalle(int id)
        {
            var dia = await _diaLaboralService.ObtenerDetalleAsync(id);
            if (dia is null) return NotFound(new { mensaje = "Día laboral no encontrado" });
            return Ok(dia);
        }

        [HttpPost("abrir")]
        public async Task<ActionResult<DiaLaboralDto>> Abrir()
        {
            var (exito, error, dia) = await _diaLaboralService.AbrirAsync();
            if (!exito) return Conflict(new { mensaje = error });
            return CreatedAtAction(nameof(ObtenerDetalle), new { id = dia!.Id }, dia);
        }

        [HttpPost("{id}/cerrar")]
        public async Task<IActionResult> Cerrar(int id)
        {
            var (exito, error) = await _diaLaboralService.CerrarAsync(id);
            if (!exito) return Conflict(new { mensaje = error });
            return NoContent();
        }
    }
}
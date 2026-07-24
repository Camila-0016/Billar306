using Billar306.Aplicacion.DTOs.Reportes;
using Billar306.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ReportesService _reportesService;

        public ReportesController(ReportesService reportesService)
        {
            _reportesService = reportesService;
        }

        [HttpGet("turno/{id}")]
        public async Task<ActionResult<TurnoReporteDto>> DetalleTurno(int id)
        {
            var detalle = await _reportesService.ObtenerDetalleTurnoAsync(id);
            if (detalle is null) return NotFound(new { mensaje = "Turno no encontrado" });
            return Ok(detalle);
        }
    }
}
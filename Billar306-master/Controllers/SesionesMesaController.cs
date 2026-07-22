using Billar306.Aplicacion.DTOs.Mesas;
using Billar306.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionesMesaController : ControllerBase
    {
        private readonly SesionMesaService _sesionMesaService;

        public SesionesMesaController(SesionMesaService sesionMesaService)
        {
            _sesionMesaService = sesionMesaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SesionMesaDto>>> ObtenerTodas()
            => Ok(await _sesionMesaService.ObtenerTodasAsync());

        [HttpGet("abiertas")]
        public async Task<ActionResult<List<SesionMesaDto>>> ObtenerAbiertas()
            => Ok(await _sesionMesaService.ObtenerAbiertasAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<SesionMesaDto>> ObtenerPorId(int id)
        {
            var sesion = await _sesionMesaService.ObtenerPorIdAsync(id);
            if (sesion is null) return NotFound(new { mensaje = "Sesión no encontrada" });
            return Ok(sesion);
        }

        [HttpPost("abrir")]
        public async Task<ActionResult<SesionMesaDto>> Abrir([FromBody] AbrirSesionMesaDto dto)
        {
            var (exito, error, noEncontrado, sesion) = await _sesionMesaService.AbrirAsync(dto);
            if (!exito)
                return noEncontrado ? NotFound(new { mensaje = error }) : Conflict(new { mensaje = error });
            return CreatedAtAction(nameof(ObtenerPorId), new { id = sesion!.Id }, sesion);
        }

        [HttpPost("{id}/cerrar")]
        public async Task<ActionResult<SesionMesaDto>> Cerrar(int id, [FromBody] CerrarSesionMesaDto dto)
        {
            var (exito, error, noEncontrado) = await _sesionMesaService.CerrarAsync(id, dto);
            if (!exito)
                return noEncontrado ? NotFound(new { mensaje = error }) : Conflict(new { mensaje = error });

            var sesionCerrada = await _sesionMesaService.ObtenerPorIdAsync(id);
            return Ok(sesionCerrada);
        }
    }
}
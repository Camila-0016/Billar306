// SalidaController.cs
using Billar306.Aplicacion.DTOs.Salida;
using Billar306.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidaController : ControllerBase
    {
        private readonly SalidaService _salidaService;

        public SalidaController(SalidaService salidaService)
        {
            _salidaService = salidaService;
        }

        [HttpGet("estado/{empleadoId}")]
        public async Task<ActionResult<EstadoSalidaDto>> ObtenerEstado(int empleadoId)
        {
            var (exito, error, estado) = await _salidaService.ObtenerEstadoAsync(empleadoId);
            if (!exito) return Conflict(new { mensaje = error });
            return Ok(estado);
        }

        [HttpPost]
        public async Task<IActionResult> Confirmar([FromBody] ConfirmarSalidaDto dto)
        {
            var (exito, error, aviso) = await _salidaService.ConfirmarSalidaAsync(dto);
            if (!exito) return Conflict(new { mensaje = error });
            return Ok(new { mensaje = aviso });
        }
    }
}
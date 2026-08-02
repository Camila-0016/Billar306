using Billar306.API.Extensions;
using Billar306.Aplicacion.DTOs.Salida;
using Billar306.Aplicacion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalidaController : ControllerBase
    {
        private readonly SalidaService _salidaService;

        public SalidaController(SalidaService salidaService)
        {
            _salidaService = salidaService;
        }

        [HttpGet("estado")]
        public async Task<ActionResult<EstadoSalidaDto>> ObtenerEstado()
        {
            var (exito, error, estado) = await _salidaService.ObtenerEstadoAsync(this.UsuarioIdActual());
            if (!exito) return Conflict(new { mensaje = error });
            return Ok(estado);
        }

        [HttpPost]
        public async Task<IActionResult> Confirmar([FromBody] ConfirmarSalidaDto dto)
        {
            var (exito, error, aviso) = await _salidaService.ConfirmarSalidaAsync(this.UsuarioIdActual(), dto.CerrarDiaLaboral);
            if (!exito) return Conflict(new { mensaje = error });
            return Ok(new { mensaje = aviso });
        }
    }
}
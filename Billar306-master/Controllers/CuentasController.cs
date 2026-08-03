using Billar306.Aplicacion.DTOs.Cuentas;
using Billar306.Aplicacion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CuentasController : ControllerBase
    {
        private readonly CuentaService _cuentaService;

        public CuentasController(CuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CuentaDetalleDto>> ObtenerDetalle(int id)
        {
            var detalle = await _cuentaService.ObtenerDetalleAsync(id);
            if (detalle is null) return NotFound(new { mensaje = "Cuenta no encontrada" });
            return Ok(detalle);
        }
    }
}
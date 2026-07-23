using Billar306.Aplicacion.DTOs.Confiteria;
using Billar306.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [Route("api/confiteria")]
    [ApiController]
    public class ConfiteriaController : ControllerBase
    {
        private readonly ConfiteriaService _confiteriaService;

        public ConfiteriaController(ConfiteriaService confiteriaService)
        {
            _confiteriaService = confiteriaService;
        }

        [HttpPost("venta-directa")]
        public async Task<ActionResult<VentaConfiteriaDto>> VentaDirecta([FromBody] CrearVentaDirectaDto dto)
        {
            var (exito, error, venta) = await _confiteriaService.CrearVentaDirectaAsync(dto);
            if (!exito) return Conflict(new { mensaje = error });
            return Ok(venta);
        }

        [HttpPost("mesa/{sesionMesaId}")]
        public async Task<ActionResult<VentaConfiteriaDto>> AgregarAMesa(int sesionMesaId, [FromBody] AgregarConsumicionMesaDto dto)
        {
            var (exito, error, noEncontrado, venta) = await _confiteriaService.AgregarAMesaAsync(sesionMesaId, dto);
            if (!exito)
                return noEncontrado ? NotFound(new { mensaje = error }) : Conflict(new { mensaje = error });
            return Ok(venta);
        }
        
        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> QuitarItem(int itemId)
        {
            var (exito, error) = await _confiteriaService.QuitarItemAsync(itemId);
            if (!exito) return Conflict(new { mensaje = error });
            return NoContent();
        }
        [HttpGet("venta/{id}")]
        public async Task<ActionResult<VentaConfiteriaDto>> ObtenerVenta(int id)
        {
            var venta = await _confiteriaService.ObtenerVentaAsync(id);
            if (venta is null) return NotFound(new { mensaje = "Venta no encontrada" });
            return Ok(venta);
        }
        [HttpGet("ventas")]
        public async Task<ActionResult<List<VentaConfiteriaDto>>> ObtenerTodas()
    => Ok(await _confiteriaService.ObtenerTodasAsync());
    }
}
using Billar306.Aplicacion.DTOs.Confiteria;
using Billar306.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoService _productoService;

        public ProductosController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductoDto>>> ObtenerTodos()
            => Ok(await _productoService.ObtenerTodosAsync());

        [HttpGet("catalogo/{catalogoId}")]
        public async Task<ActionResult<List<ProductoDto>>> ObtenerPorCatalogo(int catalogoId)
            => Ok(await _productoService.ObtenerPorCatalogoAsync(catalogoId));

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> ObtenerPorId(int id)
        {
            var producto = await _productoService.ObtenerPorIdAsync(id);
            if (producto is null) return NotFound(new { mensaje = "Producto no encontrado" });
            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDto>> Crear([FromBody] CrearProductoDto dto)
        {
            var (exito, error, producto) = await _productoService.CrearAsync(dto);
            if (!exito) return Conflict(new { mensaje = error });
            return CreatedAtAction(nameof(ObtenerPorId), new { id = producto!.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarProductoDto dto)
        {
            if (id != dto.Id) return BadRequest(new { mensaje = "Los ID no coinciden" });
            var actualizado = await _productoService.ActualizarAsync(id, dto);
            if (!actualizado) return NotFound(new { mensaje = "Producto no encontrado" });
            return NoContent();
        }
    }
}
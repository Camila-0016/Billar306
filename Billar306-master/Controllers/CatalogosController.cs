using Billar306.Aplicacion.DTOs.Confiteria;
using Billar306.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        private readonly CatalogoService _catalogoService;

        public CatalogosController(CatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CatalogoDto>>> ObtenerTodos()
            => Ok(await _catalogoService.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogoDto>> ObtenerPorId(int id)
        {
            var catalogo = await _catalogoService.ObtenerPorIdAsync(id);
            if (catalogo is null) return NotFound(new { mensaje = "Catálogo no encontrado" });
            return Ok(catalogo);
        }

        [HttpPost]
        public async Task<ActionResult<CatalogoDto>> Crear([FromBody] CrearCatalogoDto dto)
        {
            var catalogo = await _catalogoService.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = catalogo.Id }, catalogo);
        }
    }
}
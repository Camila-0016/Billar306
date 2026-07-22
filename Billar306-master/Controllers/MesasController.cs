using Billar306.Aplicacion.DTOs.Mesas;
using Billar306.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesasController : ControllerBase
    {
        private readonly MesaService _mesaService;

        public MesasController(MesaService mesaService)
        {
            _mesaService = mesaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MesaDto>>> ObtenerTodas()
            => Ok(await _mesaService.ObtenerTodasAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<MesaDto>> ObtenerPorId(int id)
        {
            var mesa = await _mesaService.ObtenerPorIdAsync(id);
            if (mesa is null) return NotFound(new { mensaje = "Mesa no encontrada" });
            return Ok(mesa);
        }

        [HttpPost]
        public async Task<ActionResult<MesaDto>> Crear([FromBody] CrearMesaDto dto)
        {
            var (exito, error, mesa) = await _mesaService.CrearAsync(dto);
            if (!exito) return Conflict(new { mensaje = error });
            return CreatedAtAction(nameof(ObtenerPorId), new { id = mesa!.Id }, mesa);
        }
    }
}
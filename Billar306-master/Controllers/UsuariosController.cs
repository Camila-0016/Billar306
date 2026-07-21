using Billar306.Aplicacion.DTOs.Usuarios;
using Billar306.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDto>>> ObtenerTodos()
        {
            var usuarios = await _usuarioService.ObtenerTodosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> ObtenerPorId(int id)
        {
            var usuario = await _usuarioService.ObtenerPorIdAsync(id);
            if (usuario is null) return NotFound(new { mensaje = "Usuario no encontrado" });
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> Crear([FromBody] CrearUsuarioDto dto)
        {
            var (exito, error, usuario) = await _usuarioService.AgregarAsync(dto);
            if (!exito) return Conflict(new { mensaje = error });
            return CreatedAtAction(nameof(ObtenerPorId), new { id = usuario!.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarUsuarioDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { mensaje = "Los ID no coinciden (el ID de la ruta es distinto al del body)" });

            var actualizado = await _usuarioService.ActualizarAsync(id, dto);
            if (!actualizado) return NotFound(new { mensaje = "Usuario no encontrado" });
            return NoContent();
        }
    }
}
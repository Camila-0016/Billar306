using Billar306.API.DTOs;
using Billar306.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize(Roles = "encargado,jefe")]
        public async Task<IActionResult> ObtenerTodos()
        {
            var usuarios = await _usuarioService.ObtenerTodosAsync();
            var resultado = usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                NombreUsuario = u.NombreUsuario,
                NombreCompleto = u.NombreCompleto,
                Rol = u.Rol,
                SueldoBase = u.SueldoBase,
                Activo = u.Activo
            });
            return Ok(resultado);
        }

        [HttpPost]
        [Authorize(Roles = "jefe")]
        public async Task<IActionResult> Crear([FromBody] CrearUsuarioRequest request)
        {
            var rolesValidos = new[] { "empleado", "encargado", "jefe" };
            if (!rolesValidos.Contains(request.Rol))
                return BadRequest(new { mensaje = "Rol inválido. Usá: empleado, encargado o jefe." });

            var (ok, error) = await _usuarioService.CrearUsuarioAsync(
                request.NombreUsuario, request.Password, request.Rol,
                request.NombreCompleto, request.SueldoBase);

            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Usuario creado correctamente." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "jefe")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var (ok, error) = await _usuarioService.DesactivarUsuarioAsync(id);
            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Usuario desactivado correctamente." });
        }
    }

    public class CrearUsuarioRequest
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public decimal SueldoBase { get; set; }
    }
}
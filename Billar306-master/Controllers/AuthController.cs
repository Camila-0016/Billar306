using Billar306.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.LoginAsync(request.NombreUsuario, request.Password);
            if (token == null)
                return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos." });

            return Ok(new { token });
        }
    }

    public class LoginRequest
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
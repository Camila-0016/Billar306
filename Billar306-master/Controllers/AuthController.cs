using Billar306.Aplicacion.DTOs.Auth;
using Billar306.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto dto)
        {
            var (exito, error, resultado) = await _authService.LoginAsync(dto);
            if (!exito) return Unauthorized(new { mensaje = error });
            return Ok(resultado);
        }
    }
}
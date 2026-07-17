using Billar306.API.Configuration;
using Billar306.API.Repositories;
using Billar306.Data.Models.Control;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Billar306.API.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IConfiguracionRepository _configRepo;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository usuarioRepo, IConfiguracionRepository configRepo, IConfiguration configuration)
        {
            _usuarioRepo = usuarioRepo;
            _configRepo = configRepo;
            _configuration = configuration;
        }

        public async Task<string?> LoginAsync(string nombreUsuario, string password)
        {
            var usuario = await _usuarioRepo.ObtenerPorNombreUsuarioAsync(nombreUsuario);
            if (usuario == null || !usuario.Activo) return null;
            if (!BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash)) return null;
            return await GenerarTokenAsync(usuario);
        }

        private async Task<string> GenerarTokenAsync(Usuario usuario)
        {
            var horas = await _configRepo.ObtenerEnteroAsync(ConfiguracionKeys.DuracionTokenHoras, 8);
            var key = _configuration["Jwt:Key"] ?? "clave_secreta_por_defecto_billar306";

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.Rol),
                new Claim("nombreCompleto", usuario.NombreCompleto)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Billar306",
                audience: "Billar306",
                claims: claims,
                expires: DateTime.Now.AddHours(horas),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

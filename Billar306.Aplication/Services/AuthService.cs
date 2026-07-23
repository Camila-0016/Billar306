using Billar306.Aplicacion.DTOs.Auth;
using Billar306.Dominio.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Billar306.Aplicacion.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<(bool Exito, string? Error, LoginResponseDto? Resultado)> LoginAsync(LoginDto dto)
        {
            var usuario = await _usuarioRepository.BuscarPorNombreUsuarioAsync(dto.NombreUsuario);

            // no revelar si falló el usuario o la contraseña
            if (usuario is null || !usuario.Activo)
                return (false, "Usuario o contraseña incorrectos.", null);

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
                return (false, "Usuario o contraseña incorrectos.", null);

            var jwtKey = _configuration["Jwt:Key"] ?? "billar306_clave_secreta_super_larga_2026";
            var expiraEn = DateTime.UtcNow.AddHours(8);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())
            };

            var credenciales = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Billar306",
                audience: "Billar306",
                claims: claims,
                expires: expiraEn,
                signingCredentials: credenciales);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return (true, null, new LoginResponseDto(tokenString, expiraEn, usuario.Id, usuario.NombreUsuario, usuario.Rol));
        }
    }
}
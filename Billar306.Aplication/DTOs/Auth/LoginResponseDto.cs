using Billar306.Dominio.Models.Control;

namespace Billar306.Aplicacion.DTOs.Auth
{
    public record LoginResponseDto(
        string Token, DateTime ExpiraEn,
        int UsuarioId, string NombreUsuario, RolUsuario Rol
    );
}
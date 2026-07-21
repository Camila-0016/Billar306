using Billar306.Dominio.Models.Control;

namespace Billar306.Aplicacion.DTOs.Usuarios
{
    public record UsuarioDto(int Id, string NombreUsuario, RolUsuario Rol, bool Activo, DateTime FechaInicio);
}
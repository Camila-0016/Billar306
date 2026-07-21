using Billar306.Dominio.Models.Control;
using System.ComponentModel.DataAnnotations;

namespace Billar306.Aplicacion.DTOs.Usuarios
{
    public record ActualizarUsuarioDto(
        int Id,

        [Required(AllowEmptyStrings = false), MaxLength(50)]
        string NombreUsuario,

        RolUsuario Rol,
        bool Activo
    );
}
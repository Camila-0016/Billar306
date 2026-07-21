using Billar306.Dominio.Models.Control;
using System.ComponentModel.DataAnnotations;

namespace Billar306.Aplicacion.DTOs.Usuarios
{
    public record CrearUsuarioDto(
        [Required(AllowEmptyStrings = false), MaxLength(50)]
        string NombreUsuario,

        [Required(AllowEmptyStrings = false)]
        string Password,

        RolUsuario Rol
    );
}
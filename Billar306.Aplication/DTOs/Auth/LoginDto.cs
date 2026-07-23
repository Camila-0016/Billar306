using System.ComponentModel.DataAnnotations;

namespace Billar306.Aplicacion.DTOs.Auth
{
    public record LoginDto(
        [Required(AllowEmptyStrings = false)] string NombreUsuario,
        [Required(AllowEmptyStrings = false)] string Password
    );
}
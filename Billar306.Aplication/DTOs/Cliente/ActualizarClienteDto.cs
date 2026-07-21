using System.ComponentModel.DataAnnotations;

namespace Billar306.Aplicacion.DTOs.Clientes
{
    public record ActualizarClienteDto(
        int Id,

        [Required(AllowEmptyStrings = false), MaxLength(100)]
        string NombreCompleto,

        bool Activo
    );
}
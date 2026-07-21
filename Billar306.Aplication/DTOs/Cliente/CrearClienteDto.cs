using System.ComponentModel.DataAnnotations;

namespace Billar306.Aplicacion.DTOs.Clientes
{
    public record CrearClienteDto(
        [Required(AllowEmptyStrings = false), MaxLength(100)]
        string NombreCompleto
    );
}
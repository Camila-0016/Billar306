using System.ComponentModel.DataAnnotations;

namespace Billar306.Aplicacion.DTOs.Confiteria
{
    public record ActualizarProductoDto(
        int Id,
        [Required, MaxLength(150)] string Nombre,
        [Range(0.01, double.MaxValue)] decimal Precio,
        [MaxLength(500)] string? Descripcion,
        bool Activo
    );
}
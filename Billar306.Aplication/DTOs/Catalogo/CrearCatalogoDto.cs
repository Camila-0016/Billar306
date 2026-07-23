using System.ComponentModel.DataAnnotations;

namespace Billar306.Aplicacion.DTOs.Confiteria
{
    public record CrearCatalogoDto([Required, MaxLength(100)] string Categoria);
}
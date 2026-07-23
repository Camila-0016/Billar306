using System.ComponentModel.DataAnnotations;

namespace Billar306.Aplicacion.DTOs.Confiteria
{
    public record ItemVentaDto(int ProductoId, [Range(1, int.MaxValue)] int Cantidad);
}
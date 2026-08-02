namespace Billar306.Aplicacion.DTOs.Confiteria
{
    public record CrearVentaDirectaDto(int? ClienteId, string? NombreClienteNuevo, List<ItemVentaDto> Items);
}
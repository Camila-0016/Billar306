namespace Billar306.Aplicacion.DTOs.Confiteria
{
    public record CrearVentaDirectaDto(
        int? ClienteId, string? NombreClienteNuevo,
        int EmpleadoId, List<ItemVentaDto> Items
    );
}
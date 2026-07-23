namespace Billar306.Aplicacion.DTOs.Confiteria
{
    public record VentaConfiteriaDto(int Id, decimal Total, List<ItemConfiteriaDto> Items);
}
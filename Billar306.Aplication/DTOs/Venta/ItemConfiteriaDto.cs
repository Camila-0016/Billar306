namespace Billar306.Aplicacion.DTOs.Confiteria
{
    public record ItemConfiteriaDto(
        int Id, int ProductoId, string Nombre, int Cantidad,
        decimal PrecioUnitario, decimal Total, DateTime FechaInicio
    );
}
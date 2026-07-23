namespace Billar306.Aplicacion.DTOs.Confiteria
{
    public record ProductoDto(int Id, string Nombre, decimal Precio, string Descripcion, int CatalogoId, bool Activo);
}
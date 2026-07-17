namespace Billar306.Data.Models.Control
{
    public class Catalogo : EntidadBase
    {
        public string? Categoria { get; set; }

        // Navegación: 
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
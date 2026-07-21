using Billar306.Dominio.Models;

namespace Billar306.Dominio.Models.Control
{
    public class Catalogo : EntidadBase
    {
        public string Categoria { get; set; } = string.Empty;

        // Navegación: 
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
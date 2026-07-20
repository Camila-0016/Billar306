using Billar306.Dominio.Models;

namespace Billar306.Dominio.Models.Control
{
    public class Producto : EntidadBase
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int CatalogoId { get; set; }

        // Propiedad de Navegación inversa
        public Catalogo Catalogo { get; set; } = null!;
    }
}

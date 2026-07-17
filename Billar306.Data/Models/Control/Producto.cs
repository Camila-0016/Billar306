namespace Billar306.Data.Models.Control
{
    public class Producto : EntidadBase
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int? StockMinimo { get; set; }
        public string? Descripcion { get; set; }
    }
}

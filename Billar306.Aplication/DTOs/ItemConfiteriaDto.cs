namespace Billar306.API.DTOs
{
    public class ItemConfiteriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public bool SinStock { get; set; }
        public bool StockBajo { get; set; }
    }
}
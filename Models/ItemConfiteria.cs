namespace Billar306.API.Models
{
    public class ItemConfiteria
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int StockActual { get; set; }
    public int StockMinimo { get; set; } = 5;
    public bool Activo { get; set; } = true;

    public int StockApertura { get; set; } = 0;

        // Navegación
        public ICollection<ConsumicionMesa> Consumiciones { get; set; } = new List<ConsumicionMesa>();
}
}
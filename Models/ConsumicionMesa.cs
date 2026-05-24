namespace Billar306.API.Models
{
    public class ConsumicionMesa
    {
        public int Id { get; set; }
        public int SesionMesaId { get; set; }
        public int ItemConfiteriaId { get; set; }
        public int Cantidad { get; set; } = 1;
        public decimal PrecioUnitario { get; set; }

        // Navegación
        public SesionMesa SesionMesa { get; set; } = null!;
        public ItemConfiteria ItemConfiteria { get; set; } = null!;
    }
}
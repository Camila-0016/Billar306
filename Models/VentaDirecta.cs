namespace Billar306.API.Models
{
    public class VentaDirecta
    {
        public int Id { get; set; }
        public int TurnoId { get; set; }
        public int UsuarioId { get; set; }
        public int ItemConfiteriaId { get; set; }
        public int Cantidad { get; set; } = 1;
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

        // Navegación
        public Turno Turno { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;
        public ItemConfiteria ItemConfiteria { get; set; } = null!;
    }
}
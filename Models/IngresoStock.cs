namespace Billar306.API.Models
{
    public class IngresoStock
    {
        public int Id { get; set; }
        public int TurnoId { get; set; }
        public int ItemConfiteriaId { get; set; }
        public int Cantidad { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public int UsuarioId { get; set; }
        public string? Nota { get; set; }

        // Navegación
        public Turno Turno { get; set; } = null!;
        public ItemConfiteria ItemConfiteria { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;
    }
}
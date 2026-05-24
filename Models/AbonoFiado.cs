namespace Billar306.API.Models
{
    public class AbonoFiado
    {
        public int Id { get; set; }
        public int FiadoId { get; set; }
        public int UsuarioId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string? Nota { get; set; }

        // Navegación
        public Fiado Fiado { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;
    }
}
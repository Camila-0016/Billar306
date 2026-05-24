namespace Billar306.API.Models
{
    public class Turno
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaApertura { get; set; } = DateTime.Now;
        public DateTime? FechaCierre { get; set; }
        public decimal MontoAperturaCaja { get; set; }
        public decimal? MontoCierreFisico { get; set; }
        public decimal? MontoEsperado { get; set; }
        public decimal? DiferenciaCaja { get; set; }
        public string GravedadDiferencia { get; set; } = string.Empty; // "baja", "media", "alta"
        public string? NotaCierre { get; set; }
        public bool AperturaCompleta { get; set; } = true;
        public string Estado { get; set; } = "Abierto"; // "Abierto", "Cerrado"
        public decimal? EfectivoConfiteria { get; set; }
        public decimal? EfectivoMaquinas { get; set; }
        // Navegación
        public Usuario Usuario { get; set; } = null!;
        public ICollection<SesionMesa> Sesiones { get; set; } = new List<SesionMesa>();
        public ICollection<Anticipo> Anticipos { get; set; } = new List<Anticipo>();
        public ICollection<EventoTurno> Eventos { get; set; } = new List<EventoTurno>();
    }
}
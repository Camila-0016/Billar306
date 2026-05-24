namespace Billar306.API.Models
{
    public class SesionMesa
    {
        public int Id { get; set; }
        public int MesaId { get; set; }
        public int TurnoId { get; set; }
        public int UsuarioId { get; set; }
        public int? ClienteFrecuenteId { get; set; }
        public DateTime Inicio { get; set; } = DateTime.Now;
        public DateTime? Fin { get; set; }
        public decimal? TotalCobrado { get; set; }
        public string Estado { get; set; } = "Abierta"; // "Abierta", "Cobrada", "Fiada"
        public decimal? MontoRecibido { get; set; }
        public decimal? Vuelto { get; set; }

        // Navegación
        public Mesa Mesa { get; set; } = null!;
        public Turno Turno { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;
        public ClienteFrecuente? ClienteFrecuente { get; set; }
        public ICollection<ConsumicionMesa> Consumiciones { get; set; } = new List<ConsumicionMesa>();
        public Fiado? Fiado { get; set; }
    }
}
namespace Billar306.API.Models
{
    public class EventoTurno
    {
        public int Id { get; set; }
        public int TurnoId { get; set; }
        public int UsuarioId { get; set; }
        public string TipoEvento { get; set; } = string.Empty;
        // "discrepancia_apertura", "diferencia_caja", "sesion_no_autorizada",
        // "anticipo_excedido", "fiado_sin_prenda"
        public string Gravedad { get; set; } = string.Empty; // "baja", "media", "alta"
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public bool Revisado { get; set; } = false;
        public string? NotaRevision { get; set; }
        public int? UsuarioRevisionId { get; set; }

        // Navegación
        public Turno Turno { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;
    }
}
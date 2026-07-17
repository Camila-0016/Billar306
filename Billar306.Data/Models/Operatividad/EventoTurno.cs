using Billar306.Data.Models.Control;

namespace Billar306.Data.Models.Operatividad
{
    public class EventoTurno : EntidadBase
    {
        public int TurnoId { get; set; }
        public int EmpleadoId { get; set; }
        public string NombreEvento { get; set; } = string.Empty;
        // "discrepancia_apertura", "diferencia_caja", "sesion_no_autorizada",
        // "anticipo_excedido", "fiado_sin_prenda"
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaRevisado { get; set; } = DateTime.Now;
        public bool Revisado { get; set; } = false;
        public string? NotaRevision { get; set; }
        public int? UsuarioRevisionId { get; set; }

        // Navegación
        public Turno Turno { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;
    }
}
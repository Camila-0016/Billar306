using Billar306.Dominio.Models;
using Billar306.Dominio.Models.Control;

namespace Billar306.Dominio.Models.Operatividad
{
    public class EventoTurno : EntidadBase
    {
        public int TurnoId { get; set; }
        public int EmpleadoId { get; set; }
        public TipoEventoTurno NombreEvento { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime? FechaRevisado { get; set; } = DateTime.Now;
        public bool Revisado { get; set; } = false;
        public string? NotaRevision { get; set; }
        public int? UsuarioRevisionId { get; set; }

        // Navegación
        public Turno Turno { get; set; } = null!;
        public Usuario Empleado { get; set; } = null!;
        public Usuario? UsuarioRevision { get; set; }
    }
}
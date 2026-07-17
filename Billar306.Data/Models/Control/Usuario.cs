using Billar306.Data.Models.Empleado;
using Billar306.Data.Models.Operatividad;

namespace Billar306.Data.Models.Control
{
    public class Usuario : EntidadBase
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int RolId { get; set; } // "empleado", "encargado", "jefe"

        // Navegación
        public ConfiguracionSistema Rol { get; set; } = null!;
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
        public ICollection<Anticipo>? Anticipos { get; set; } = new List<Anticipo>();
        public ICollection<Amonestacion>? Amonestaciones { get; set; } = new List<Amonestacion>();
    }
}
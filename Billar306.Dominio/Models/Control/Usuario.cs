using Billar306.Dominio.Models.Empleado;
using Billar306.Dominio.Models.Operatividad;

namespace Billar306.Dominio.Models.Control
{
    public class Usuario : EntidadBase
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public RolUsuario Rol { get; set; } // "empleado", "encargado", "jefe"

        // Navegación
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
        public ICollection<Anticipo> Anticipos { get; set; } = new List<Anticipo>();
        public ICollection<Amonestacion> Amonestaciones { get; set; } = new List<Amonestacion>();
    }
}
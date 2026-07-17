using Billar306.Data.Models.Control;

namespace Billar306.Data.Models.Operatividad
{
    public class DiaLaboral : EntidadBase
    {
        public DateTime? FechaCierre { get; set; }
        public string Estado { get; set; } = "Abierto"; // "Abierto", "Cerrado"

        // Navegación
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
    }
}
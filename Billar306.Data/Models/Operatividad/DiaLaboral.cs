
namespace Billar306.Dominio.Models.Operatividad
{
    public class DiaLaboral : EntidadBase
    {
        public DateTime? FechaCierre { get; set; }
        public bool EstaCerrado { get; set; } = false;

        // Navegación
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
    }
}
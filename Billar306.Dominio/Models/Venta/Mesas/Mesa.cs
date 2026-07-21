using Billar306.Dominio.Models;

namespace Billar306.Dominio.Models.Venta.Mesas
{
    public class Mesa : EntidadBase
    {
        public int Numero { get; set; }
        public bool Ocupada { get; set; } // true = "Ocupada", false = "Libre"

        // Navegación
        public ICollection<SesionMesa> Sesiones { get; set; } = new List<SesionMesa>();
    }
}
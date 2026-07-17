using Billar306.Data.Models;

namespace Billar306.Data.Models.Venta.Maquina
{
    public class Maquina : EntidadBase
    {
        public string Identificador { get; set; }
        public string Estado { get; set; } = "Libre"; // "Libre", "Ocupada"

        // Navegación
        public ICollection<SesionMaquina> Sesiones { get; set; } = new List<SesionMaquina>();
    }
}

using Billar306.Dominio.Models;

namespace Billar306.Dominio.Models.Venta.Maquina
{
    public class Maquina : EntidadBase
    {
        public string Identificador { get; set; } = string.Empty;
        public bool EstaOcupada { get; set; } = false;

        // Navegación
        public ICollection<SesionMaquina> Sesiones { get; set; } = new List<SesionMaquina>();
    }
}

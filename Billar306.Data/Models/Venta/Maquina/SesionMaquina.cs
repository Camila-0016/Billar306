using Billar306.Data.Models.Control;
using Billar306.Data.Models.Operatividad;
using Billar306.Data.Models.Venta;

namespace Billar306.Data.Models.Venta.Maquina
{
    public class SesionMaquina :CuentaBase
    {
        public int MaquinaId { get; set; }
        public DateTime? FechaFin { get; set; } = DateTime.Now;
        public decimal? Total { get; set; }

        // Navegación
        public Maquina Maquina { get; set; } = null!;
        public ICollection<TransaccionMaquina>? Transacciones { get; set; } = new List<TransaccionMaquina>(); 
    }
}
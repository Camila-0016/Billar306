using Billar306.Data.Models.Control;
using Billar306.Data.Models.Operatividad;
using Billar306.Data.Models.Venta;

namespace Billar306.Data.Models.Venta.Mesa
{
    public class SesionMesa : CuentaBase
    {
        public int MesaId { get; set; }
        public DateTime? FechaFin { get; set; } = DateTime.Now;
        public decimal MontoSesionMesa { get; set; }

        // Navegación
        public Mesa Mesa { get; set; } = null!;
    }
}
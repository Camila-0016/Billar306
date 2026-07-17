using Billar306.Data.Models.Control;
using Billar306.Data.Models.Venta;
using Billar306.Data.Models.Venta.Mesa;

namespace Billar306.Data.Models.Clientes
{
    public class Prenda: EntidadBase
    {
        public int ClienteId { get; set; }
        public int CuentaId { get; set; }
        public int EmpleadoResponsableId { get; set; }
        public string DescripcionPrenda { get; set; }
        public decimal MontoPrenda { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Estado { get; set; } = "Pendiente"; // "Pendiente","Cobrado","Vencido"
       

        // Navegación
        public Cliente Cliente { get; set; } = null!;
        public Usuario EmpleadoResponsable { get; set; } = null!;
        public ICollection<CobroDeuda>? Abonos { get; set; } = new List<CobroDeuda>();
        public CuentaBase Cuenta { get; set; } = null!;
    }
}
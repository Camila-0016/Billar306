using Billar306.Dominio.Models.Control;
using Billar306.Dominio.Models.Venta;

namespace Billar306.Dominio.Models.Clientes
{
    public class Prenda: EntidadBase
    {
        public int ClienteId { get; set; }
        public int CuentaId { get; set; }
        public int EmpleadoResponsableId { get; set; }
        public string DescripcionPrenda { get; set; } = string.Empty;
        public decimal MontoPrenda { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public EstadoPrenda Estado { get; set; } = EstadoPrenda.Pendiente;

        // Navegación
        public Cliente Cliente { get; set; } = null!;
        public Usuario EmpleadoResponsable { get; set; } = null!;
        public ICollection<CobroDeuda> Abonos { get; set; } = new List<CobroDeuda>();
        public CuentaBase Cuenta { get; set; } = null!;
    }
}
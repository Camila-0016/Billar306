using Billar306.Data.Models.Venta;
using Billar306.Data.Models.Venta.Mesa;

namespace Billar306.Data.Models.Clientes
{
    public class Cliente : EntidadBase
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public bool CreditoHabilitado { get; set; } = false;
        public decimal MontoCredito { get; set; } 

        // Navegación
        public ICollection<CuentaBase> Cuentas { get; set; } = new List<CuentaBase>();
        public ICollection<Prenda> Prendas { get; set; } = new List<Prenda>();
    }
}
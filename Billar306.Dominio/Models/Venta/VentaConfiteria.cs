using Billar306.Dominio.Models.Operatividad;

namespace Billar306.Dominio.Models.Venta
{
    public class VentaConfiteria : EntidadBase
    {
        public decimal Total { get; set; }


        // Navegación inversa opcional hacia CuentaBase (si la venta se carga a una mesa)
        public CuentaBase? CuentaAsociada { get; set; }
        // Navegación
        public ICollection<ItemConfiteria> ItemsConfiterias { get; set; } = null!;
        
    }
}
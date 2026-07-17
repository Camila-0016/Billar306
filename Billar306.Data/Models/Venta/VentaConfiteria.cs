using Billar306.Data.Models.Control;
using Billar306.Data.Models.Operatividad;

namespace Billar306.Data.Models.Venta
{
    public class VentaConfiteria : EntidadBase
    {
        public decimal Total { get; set; }

        // Navegación
        public ICollection<ItemConfiteria> ItemsConfiterias { get; set; } = null!;
        
    }
}
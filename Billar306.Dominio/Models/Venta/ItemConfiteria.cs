using Billar306.Dominio.Models.Control;

namespace Billar306.Dominio.Models.Venta
{
    public class ItemConfiteria : EntidadBase
    {
        public int VentaConfiteriaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public int ProductoId { get; set; }

        //Navegacion
        public VentaConfiteria Venta { get; set; } = null!;
        public Producto Producto { get; set; } = null!;
    }
}
using Billar306.Data.Models.Control;

namespace Billar306.Data.Models.Venta
{
    public class ItemConfiteria : EntidadBase
    {
        public string Nombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public int ProductoId { get; set; }

        //Navegacion
        public Producto Producto { get; set; } = null!;
    }
}
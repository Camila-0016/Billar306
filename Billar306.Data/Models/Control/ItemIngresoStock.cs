using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billar306.Data.Models.Control
{
    public class ItemIngresoStock : EntidadBase
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        //Navegacion
        public Producto Producto {  get; set; } = null!;
    }
}

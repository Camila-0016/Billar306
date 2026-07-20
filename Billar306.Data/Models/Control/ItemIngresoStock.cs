using Billar306.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billar306.Dominio.Models.Control
{
    public class ItemIngresoStock : EntidadBase
    {
        public int IngresoStockId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        //Navegacion
        public IngresoStock IngresoStock { get; set; } = null!;
        public Producto Producto {  get; set; } = null!;
    }
}

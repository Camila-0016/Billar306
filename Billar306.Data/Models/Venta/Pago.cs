using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billar306.Data.Models.Venta
{
    public class Pago : EntidadBase
    {
        public int CuentaId { get; set; }
        public FormaPago Metodo { get; set; }
        public bool PagoParcial { get; set; } = false;
        public decimal Monto { get; set; }
    }
}

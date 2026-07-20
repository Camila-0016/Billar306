using Billar306.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billar306.Dominio.Models.Venta
{
    public class Pago : EntidadBase
    {
        public int CuentaId { get; set; }
        public FormaPago Metodo { get; set; }
        public bool PagoParcial { get; set; } = false;
        public decimal Monto { get; set; }

        public CuentaBase Cuenta { get; set; } = null!;
    }
}

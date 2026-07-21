using Billar306.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billar306.Dominio.Models.Venta.Maquina
{
    public class TransaccionMaquina : EntidadBase
    {
        public decimal Monto { get; set; }
        public bool EsIngreso { get; set; }
        public int SesionId { get; set; }

        // Navegación
        public SesionMaquina Sesion { get; set; } = null!;
    }
}

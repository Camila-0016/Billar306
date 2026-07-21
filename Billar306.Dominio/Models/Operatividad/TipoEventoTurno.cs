using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billar306.Dominio.Models.Operatividad
{
    public enum TipoEventoTurno
    {
        DiscrepanciaApertura = 1,
        DiferenciaCaja = 2,
        SesionNoAutorizada = 3,
        AnticipoExcedido = 4,
        FiadoSinPrenda = 5
    }
}

using Billar306.Dominio.Models;
using Billar306.Dominio.Models.Control;

namespace Billar306.Dominio.Models.Empleado
{
    public class Amonestacion : EntidadBase
    {
        public int EmpleadoId { get; set; }
        public GravedadAmonestacion Gravedad { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Duracion { get; set; }
        public decimal? Monto { get; set; }
        //Navegacion
        public Usuario Empleado { get; set; } = null!;
    }
}

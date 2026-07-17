using Billar306.Data.Models.Empleado;
using Billar306.Data.Models.Operatividad;
using Billar306.Data.Models.Venta;

namespace Billar306.Data.Models.Control
{
    public class IngresoStock : EntidadBase
    {
        public int TurnoId { get; set; }
        public int EmpleadoId { get; set; }


        // Navegación
        public Turno TurnoEmpleado { get; set; } = null!;
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
        public Usuario Empleado { get; set; } = null!;
    }
}
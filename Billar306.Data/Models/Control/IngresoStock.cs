using Billar306.Dominio.Models.Operatividad;

namespace Billar306.Dominio.Models.Control
{
    public class IngresoStock : EntidadBase
    {
        public int TurnoId { get; set; }
        public int EmpleadoId { get; set; }


        // Navegación
        public Turno TurnoEmpleado { get; set; } = null!;
        public ICollection<ItemIngresoStock> Productos { get; set; } = new List<ItemIngresoStock>();
        public Usuario Empleado { get; set; } = null!;
    }
}
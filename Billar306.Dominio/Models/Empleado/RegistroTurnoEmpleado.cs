using Billar306.Dominio.Models;
using Billar306.Dominio.Models.Control;
using Billar306.Dominio.Models.Operatividad;

namespace Billar306.Dominio.Models.Empleado
{
    public class RegistroTurnoEmpleado: EntidadBase
    {
        public int EmpleadoId { get; set; }
        public int TurnoId { get; set; }
        public DateTime? Salida { get; set; }
        public decimal? HorasTrabajadas { get; set; }
        public bool Limpieza { get; set; } = false;
        public decimal? Comisiones { get; set; }
        public decimal? Descuentos { get; set; }

        // Navegación
        public Usuario Empleado { get; set; } = null!;
        public Turno Turno { get; set; } = null!;
    }
}
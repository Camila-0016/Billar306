using Billar306.Dominio.Models.Control;
using Billar306.Dominio.Models.Operatividad;

namespace Billar306.Dominio.Models.Empleado
{
    public class Anticipo: EntidadBase
    {
        public int EmpleadoId { get; set; }
        public int UsuarioAutorizanteId { get; set; }
        public decimal Monto { get; set; }
        public bool ForzadoPorJefe { get; set; } = false;

        // Navegación
        public Usuario Empleado { get; set; } = null!;
        public Usuario UsuarioAutorizante { get; set; } = null!;
        public Turno Turno { get; set; } = null!;
    }
}
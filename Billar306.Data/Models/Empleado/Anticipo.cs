using Billar306.Data.Models.Control;
using Billar306.Data.Models.Operatividad;

namespace Billar306.Data.Models.Empleado
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
    }
}
using Billar306.Dominio.Models;
using Billar306.Dominio.Models.Control;

namespace Billar306.Dominio.Models.Clientes
{
    public class CobroDeuda : EntidadBase
    {
        public int PrendaId { get; set; }
        public int EmpleadoId { get; set; }
        public decimal Monto { get; set; }
        public string? Nota { get; set; }

        // Navegación
        public Prenda Prenda { get; set; } = null!;
        public Usuario Empleado { get; set; } = null!;
    }
}
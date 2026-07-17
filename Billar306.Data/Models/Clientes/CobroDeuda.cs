using Billar306.Data.Models.Control;

namespace Billar306.Data.Models.Clientes
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
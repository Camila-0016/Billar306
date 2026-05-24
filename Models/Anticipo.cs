namespace Billar306.API.Models
{
    public class Anticipo
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public int TurnoId { get; set; }
        public int UsuarioAutorizanteId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public bool ForzadoPorJefe { get; set; } = false;

        // Navegación
        public Usuario Empleado { get; set; } = null!;
        public Turno Turno { get; set; } = null!;
        public Usuario UsuarioAutorizante { get; set; } = null!;
    }
}

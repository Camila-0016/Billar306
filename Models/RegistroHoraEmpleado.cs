namespace Billar306.API.Models
{
    public class RegistroHoraEmpleado
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int TurnoId { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime? Salida { get; set; }
        public decimal? HorasTrabajadas { get; set; }

        // Navegación
        public Usuario Usuario { get; set; } = null!;
        public Turno Turno { get; set; } = null!;
    }
}
namespace Billar306.API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty; // "empleado", "encargado", "jefe"
        public string NombreCompleto { get; set; } = string.Empty;
        public decimal SueldoBase { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // Navegación
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
        public ICollection<Anticipo> Anticipos { get; set; } = new List<Anticipo>();
    }
}
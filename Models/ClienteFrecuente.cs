namespace Billar306.API.Models
{
    public class ClienteFrecuente
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Navegación
        public ICollection<SesionMesa> Sesiones { get; set; } = new List<SesionMesa>();
        public ICollection<Fiado> Fiados { get; set; } = new List<Fiado>();
    }
}
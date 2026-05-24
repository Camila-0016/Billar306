namespace Billar306.API.Models
{
    public class Mesa
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Estado { get; set; } = "Libre"; // "Libre", "Ocupada"

        // Navegación
        public ICollection<SesionMesa> Sesiones { get; set; } = new List<SesionMesa>();
    }
}
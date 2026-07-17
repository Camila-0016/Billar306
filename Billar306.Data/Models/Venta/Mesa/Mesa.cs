namespace Billar306.Data.Models.Venta.Mesa
{
    public class Mesa : EntidadBase
    {
        public int Numero { get; set; }
        public string Estado { get; set; } = "Libre"; // "Libre", "Ocupada"

        // Navegación
        public ICollection<SesionMesa> Sesiones { get; set; } = new List<SesionMesa>();
    }
}
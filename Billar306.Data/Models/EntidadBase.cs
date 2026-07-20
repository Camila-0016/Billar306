namespace Billar306.Dominio.Models
{
    public abstract class EntidadBase
    {
        public int Id { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaInicio { get; set; } = DateTime.UtcNow;
    }
}

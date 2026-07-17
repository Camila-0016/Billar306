namespace Billar306.Data.Models
{
    public abstract class EntidadBase
    {
        public int Id { get; set; }
        public bool Activo { get; set; } = true;
        public virtual DateTime FechaInicio { get; set; } = DateTime.Now;

    }
}

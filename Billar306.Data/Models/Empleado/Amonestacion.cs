namespace Billar306.Data.Models.Empleado
{
    public class Amonestacion : EntidadBase
    {
        public int EmpleadoId { get; set; }
        public string Gravedad { get; set; }
        public string? Descripcion { get; set; }
        public int Duracion { get; set; }

        public decimal? Monto { get; set; }
    }
}

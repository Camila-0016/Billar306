namespace Billar306.API.DTOs
{
    public class AnticipoDto
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public bool ForzadoPorJefe { get; set; }
        public string? Empleado { get; set; }
        public string? Autorizante { get; set; }
    }
}
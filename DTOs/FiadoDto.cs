namespace Billar306.API.DTOs
{
    public class FiadoDto
    {
        public int Id { get; set; }
        public decimal MontoTotal { get; set; }
        public string Prenda { get; set; } = string.Empty;
        public string? DescripcionPrenda { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaPago { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? Cliente { get; set; }
        public string? RegistradoPor { get; set; }
    }
}
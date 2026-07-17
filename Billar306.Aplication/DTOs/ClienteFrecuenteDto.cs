namespace Billar306.API.DTOs
{
    public class ClienteFrecuenteDto
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Telefono { get; set; }
    }

    public class ClienteResumenDto
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public bool TieneFiadoVencido { get; set; }
        public int FiadosPendientes { get; set; }
        public decimal TotalFiadoPendiente { get; set; }
    }
}
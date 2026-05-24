namespace Billar306.API.Models
{
    public class Fiado
    {
        public int Id { get; set; }
        public int? SesionMesaId { get; set; }
        public int ClienteFrecuenteId { get; set; }
        public int UsuarioRegistroId { get; set; }
        public decimal MontoTotal { get; set; }
        public string Prenda { get; set; } = string.Empty; 
        public string? DescripcionPrenda { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaPago { get; set; }
        public string Estado { get; set; } = "Pendiente"; 
        public int? UsuarioCierreId { get; set; }
        public decimal MontoAbonado { get; set; } = 0;
        public decimal MontoPendiente => MontoTotal - MontoAbonado;
        // Navegación
        public SesionMesa SesionMesa { get; set; } = null!;
        public ClienteFrecuente ClienteFrecuente { get; set; } = null!;
        public Usuario UsuarioRegistro { get; set; } = null!;
        public ICollection<AbonoFiado> Abonos { get; set; } = new List<AbonoFiado>();
    }
}
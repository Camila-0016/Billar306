namespace Billar306.Dominio.Models.Venta.Mesas
{
    public class SesionMesa : CuentaBase
    {
        public int MesaId { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal MontoSesionMesa { get; set; }

        // Navegación
        public Mesa Mesa { get; set; } = null!;
    }
}
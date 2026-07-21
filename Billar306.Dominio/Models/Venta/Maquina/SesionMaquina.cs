namespace Billar306.Dominio.Models.Venta.Maquina
{
    public class SesionMaquina :CuentaBase
    {
        public int MaquinaId { get; set; }
        public DateTime? FechaFin { get; set; } = DateTime.Now;
        public decimal TotalIngresos { get; set; }
        public decimal TotalEgresos { get; set; }

        // Navegación
        public Maquina Maquina { get; set; } = null!;
        public ICollection<TransaccionMaquina> Transacciones { get; set; } = new List<TransaccionMaquina>(); 
    }
}
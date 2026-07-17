using Billar306.Data.Models.Control;
using Billar306.Data.Models.Venta;
using Billar306.Data.Models.Venta.Mesa;

namespace Billar306.Data.Models.Operatividad
{
    public class Turno : EntidadBase
    {
        public int DiaLaboralId { get; set; }
        public int TitularId { get; set; }
        public int? AuxiliarId { get; set; }
        public DateTime? Salida { get; set; } = DateTime.Now;

        // Campos disponibles durante el turno
        public decimal TotalMaquinas { get; set; } = 0;

        // Se completan al cerrar
        public decimal? TotalMesas { get; set; }
        public decimal? TotalConfiteria { get; set; }
        public decimal? TotalDeuda { get; set; }
        public decimal? MontoEsperado { get; set; }
        public decimal? MontoContado { get; set; }
        public decimal? Diferencia { get; set; }
        public string? GravedadDiferencia { get; set; }
        public string? NotaCierre { get; set; }

        // Navegación
        public DiaLaboral DiaLaboral { get; set; } = null!;
        public Usuario Titular { get; set; } = null!;
        public Usuario Auxiliar { get; set; } = null!;
        public ICollection<SesionMesa> Sesiones { get; set; } = new List<SesionMesa>();
        public ICollection<Notificacion> Eventos { get; set; } = new List<Notificacion>();
        public ICollection<VentaConfiteria> VentasDirectas { get; set; } = new List<VentaConfiteria>();
        public ICollection<IngresoStock> IngresosStock { get; set; } = new List<IngresoStock>();
    }
}
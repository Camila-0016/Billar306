using Billar306.Dominio.Models;
using Billar306.Dominio.Models.Control;
using Billar306.Dominio.Models.Venta;
using Billar306.Dominio.Models.Venta.Mesas;

namespace Billar306.Dominio.Models.Operatividad
{
    public class Turno : EntidadBase
    {
        public int DiaLaboralId { get; set; }
        public int TitularId { get; set; }
        public int? AuxiliarId { get; set; }

        public DateTime? Salida { get; set; }

        // Campos disponibles durante el turno
        public decimal? TotalMaquinas { get; set; } = 0;

        // Se completan al cerrar
        public decimal? TotalMesas { get; set; }
        public decimal? TotalConfiteria { get; set; }
        public decimal? TotalDeuda { get; set; }
        public decimal? MontoEsperado { get; set; }
        public decimal? MontoContado { get; set; }
        public decimal? Diferencia { get; set; }
        public NivelGravedad GravedadDiferencia { get; set; }
        public string? NotaCierre { get; set; }

        // Navegación
        public DiaLaboral DiaLaboral { get; set; } = null!;
        public Usuario Titular { get; set; } = null!;
        public Usuario? Auxiliar { get; set; } = null!;
        public ICollection<EventoTurno> Eventos { get; set; } = new List<EventoTurno>();
        public ICollection<CuentaBase> CuentasAbiertas { get; set; } = new List<CuentaBase>();
        public ICollection<IngresoStock> IngresosStock { get; set; } = new List<IngresoStock>();
    }
}
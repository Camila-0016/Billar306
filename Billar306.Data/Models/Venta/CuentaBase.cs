using Billar306.Dominio.Models.Clientes;
using Billar306.Dominio.Models.Control;
using Billar306.Dominio.Models.Operatividad;

namespace Billar306.Dominio.Models.Venta
{
    public class CuentaBase : EntidadBase
    {
        public int ClienteId { get; set; }
        public int TurnoId { get; set; }
        public int EmpleadoAperturaId { get; set; }
        public int? EmpleadoCierreId { get; set; }
        public int? VentaConfiteriaId { get; set; }
        public decimal Total { get; set; }
        // Navegación
        public Prenda? PrendaGenerada { get; set; }
        public Turno Turno { get; set; } = null!;
        public Usuario EmpleadoApertura { get; set; } = null!;
        public Usuario? EmpleadoCierre { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public VentaConfiteria? Confiteria { get; set; }

        //Relación 1:N hacia Pagos
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
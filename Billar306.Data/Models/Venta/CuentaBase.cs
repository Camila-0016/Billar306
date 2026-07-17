using Billar306.API.Models.Clientes;
using Billar306.Data.Models.Clientes;
using Billar306.Data.Models.Control;
using Billar306.Data.Models.Operatividad;

namespace Billar306.Data.Models.Venta
{
    public class CuentaBase : EntidadBase
    {
        public int ClienteId { get; set; }
        public int TurnoId { get; set; }
        public int EmpleadoAperturaId { get; set; }
        public int? EmpleadoCierreId { get; set; }
        public int PagoId { get; set; }
        public int? VentaConfiteriaId { get; set; }
        public decimal Total { get; set; }

        // Navegación
        public Turno Turno { get; set; } = null!;
        public Usuario EmpleadoApertura { get; set; } = null!;
        public Usuario? EmpleadoCierre { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public VentaConfiteria? Confiteria { get; set; }
}

using Billar306.Dominio.Models.Venta;

namespace Billar306.Dominio.Interfaces
{
    public interface ICuentaBaseRepository : IRepository<CuentaBase>
    {
        Task<CuentaBase?> ObtenerPorVentaConfiteriaIdAsync(int ventaConfiteriaId);
        Task<IEnumerable<CuentaBase>> ObtenerPorTurnoAsync(int turnoId);
    }
}
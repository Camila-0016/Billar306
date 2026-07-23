using Billar306.Dominio.Models.Venta;

namespace Billar306.Dominio.Interfaces
{
    public interface IVentaConfiteriaRepository : IRepository<VentaConfiteria>
    {
        Task<VentaConfiteria?> ObtenerConItemsAsync(int id);
        Task<IEnumerable<VentaConfiteria>> ObtenerTodasConItemsAsync();
    }
}
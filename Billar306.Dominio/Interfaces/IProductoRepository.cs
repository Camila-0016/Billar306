using Billar306.Dominio.Models.Control;

namespace Billar306.Dominio.Interfaces
{
    public interface IProductoRepository : IRepository<Producto>
    {
        Task<IEnumerable<Producto>> ObtenerPorCatalogoAsync(int catalogoId);
    }
}
using Billar306.API.Data;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Control;
using Microsoft.EntityFrameworkCore;

namespace Billar306.Persistencia.Repositories
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        public ProductoRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Producto>> ObtenerPorCatalogoAsync(int catalogoId)
            => await _dbSet.Where(p => p.CatalogoId == catalogoId).ToListAsync();
    }
}
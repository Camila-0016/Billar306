using Billar306.API.Data;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Venta;
using Microsoft.EntityFrameworkCore;

namespace Billar306.Persistencia.Repositories
{
    public class VentaConfiteriaRepository : Repository<VentaConfiteria>, IVentaConfiteriaRepository
    {
        public VentaConfiteriaRepository(AppDbContext context) : base(context) { }

        public async Task<VentaConfiteria?> ObtenerConItemsAsync(int id)
            => await _dbSet.Include(v => v.ItemsConfiterias).FirstOrDefaultAsync(v => v.Id == id);
        public async Task<IEnumerable<VentaConfiteria>> ObtenerTodasConItemsAsync()
        => await _dbSet.Include(v => v.ItemsConfiterias).ToListAsync();
    }

}
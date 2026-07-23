using Billar306.API.Data;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Venta;
using Microsoft.EntityFrameworkCore;

namespace Billar306.Persistencia.Repositories
{
    public class CuentaBaseRepository : Repository<CuentaBase>, ICuentaBaseRepository
    {
        public CuentaBaseRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<CuentaBase?> ObtenerPorVentaConfiteriaIdAsync(int ventaConfiteriaId)
            => await _dbSet.FirstOrDefaultAsync(c => c.VentaConfiteriaId == ventaConfiteriaId);
    }
}
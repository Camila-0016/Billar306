using Billar306.API.Data;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Venta.Mesas;
using Microsoft.EntityFrameworkCore;

namespace Billar306.Persistencia.Repositories
{
    public class SesionMesaRepository : Repository<SesionMesa>, ISesionMesaRepository
    {
        public SesionMesaRepository(AppDbContext context) : base(context) { }

        public async Task<SesionMesa?> ObtenerAbiertaPorMesaAsync(int mesaId)
            => await _dbSet.FirstOrDefaultAsync(s => s.MesaId == mesaId && s.FechaFin == null);

        public async Task<IEnumerable<SesionMesa>> ObtenerAbiertasAsync()
            => await _dbSet.Where(s => s.FechaFin == null).ToListAsync();
    }
}
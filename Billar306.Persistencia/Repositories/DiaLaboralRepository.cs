using Billar306.API.Data;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Operatividad;
using Microsoft.EntityFrameworkCore;

namespace Billar306.Persistencia.Repositories
{
    public class DiaLaboralRepository : Repository<DiaLaboral>, IDiaLaboralRepository
    {
        public DiaLaboralRepository(AppDbContext context) : base(context) { }

        public async Task<DiaLaboral?> ObtenerAbiertoActualAsync()
            => await _dbSet
                .Where(d => !d.EstaCerrado)
                .OrderByDescending(d => d.FechaInicio)
                .FirstOrDefaultAsync();

        public async Task<DiaLaboral?> ObtenerConTurnosAsync(int id)
            => await _dbSet
                .Include(d => d.Turnos)
                .FirstOrDefaultAsync(d => d.Id == id);
    }
}
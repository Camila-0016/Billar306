using Billar306.API.Data;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Operatividad;
using Microsoft.EntityFrameworkCore;

namespace Billar306.Persistencia.Repositories
{
    public class TurnoRepository : Repository<Turno>, ITurnoRepository
    {
        public TurnoRepository(AppDbContext context) : base(context) { }

        public async Task<Turno?> ObtenerTurnoAbiertoAsync()
            => await _dbSet.FirstOrDefaultAsync(t => t.Salida == null);
    }
}
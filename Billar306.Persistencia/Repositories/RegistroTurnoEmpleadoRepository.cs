using Billar306.API.Data;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Empleado;
using Microsoft.EntityFrameworkCore;

namespace Billar306.Persistencia.Repositories
{
    public class RegistroTurnoEmpleadoRepository : Repository<RegistroTurnoEmpleado>, IRegistroTurnoEmpleadoRepository
    {
        public RegistroTurnoEmpleadoRepository(AppDbContext context) : base(context) { }

        public async Task<RegistroTurnoEmpleado?> ObtenerAbiertoAsync(int turnoId, int empleadoId)
            => await _dbSet.FirstOrDefaultAsync(r =>
                r.TurnoId == turnoId && r.EmpleadoId == empleadoId && r.Salida == null);

        public async Task<IEnumerable<RegistroTurnoEmpleado>> ObtenerAbiertosPorTurnoAsync(int turnoId)
            => await _dbSet.Where(r => r.TurnoId == turnoId && r.Salida == null).ToListAsync();

        public async Task<IEnumerable<RegistroTurnoEmpleado>> ObtenerPorTurnoAsync(int turnoId)
            => await _dbSet.Where(r => r.TurnoId == turnoId).ToListAsync();

        public async Task<IEnumerable<RegistroTurnoEmpleado>> ObtenerPorEmpleadoAsync(int empleadoId, DateTime? desde, DateTime? hasta)
        {
            var query = _dbSet.Where(r => r.EmpleadoId == empleadoId);

            if (desde is not null)
                query = query.Where(r => r.FechaInicio >= desde.Value);

            if (hasta is not null)
                query = query.Where(r => r.FechaInicio <= hasta.Value);

            return await query.OrderByDescending(r => r.FechaInicio).ToListAsync();
        }
    }
}
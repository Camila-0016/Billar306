using Billar306.API.Data;
using Billar306.Data.Models.Empleado;
using Microsoft.EntityFrameworkCore;

namespace Billar306.API.Repositories
{
    public class AnticipoRepository : IAnticipoRepository
    {
        private readonly AppDbContext _context;

        public AnticipoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Anticipo>> ObtenerPorEmpleadoAsync(int empleadoId)
            => await _context.Anticipos
                .Include(a => a.Empleado)
                .Include(a => a.UsuarioAutorizante)
                .Where(a => a.EmpleadoId == empleadoId)
                .OrderByDescending(a => a.Fecha)
                .ToListAsync();

        public async Task<List<Anticipo>> ObtenerTodosAsync()
            => await _context.Anticipos
                .Include(a => a.Empleado)
                .Include(a => a.UsuarioAutorizante)
                .OrderByDescending(a => a.Fecha)
                .ToListAsync();

        public async Task<decimal> ObtenerAcumuladoPeriodoAsync(int empleadoId, DateTime desde)
            => await _context.Anticipos
                .Where(a => a.EmpleadoId == empleadoId && a.Fecha >= desde)
                .SumAsync(a => a.Monto);

        public async Task AgregarAsync(Anticipo anticipo)
            => await _context.Anticipos.AddAsync(anticipo);

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}
using Billar306.API.Data;
using Billar306.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Billar306.API.Repositories
{
    public class IngresoStockRepository : IIngresoStockRepository
    {
        private readonly AppDbContext _context;

        public IngresoStockRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<IngresoStock>> ObtenerPorTurnoAsync(int turnoId)
            => await _context.IngresosStock
                .Include(i => i.ItemConfiteria)
                .Include(i => i.Usuario)
                .Where(i => i.TurnoId == turnoId)
                .OrderBy(i => i.Timestamp)
                .ToListAsync();

        public async Task AgregarAsync(IngresoStock ingreso)
            => await _context.IngresosStock.AddAsync(ingreso);

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}
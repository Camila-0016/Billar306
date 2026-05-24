using Billar306.API.Data;
using Billar306.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Billar306.API.Repositories
{
    public class VentaDirectaRepository : IVentaDirectaRepository
    {
        private readonly AppDbContext _context;

        public VentaDirectaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<VentaDirecta>> ObtenerPorTurnoAsync(int turnoId)
            => await _context.VentasDirectas
                .Include(v => v.ItemConfiteria)
                .Include(v => v.Usuario)
                .Where(v => v.TurnoId == turnoId)
                .OrderByDescending(v => v.Timestamp)
                .ToListAsync();

        public async Task AgregarAsync(VentaDirecta venta)
            => await _context.VentasDirectas.AddAsync(venta);

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}
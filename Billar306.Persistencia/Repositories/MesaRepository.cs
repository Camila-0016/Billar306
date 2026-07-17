using Billar306.API.Data;
using Billar306.Data.Models.Venta.Mesa;
using Microsoft.EntityFrameworkCore;

namespace Billar306.API.Repositories
{
    public class MesaRepository : IMesaRepository
    {
        private readonly AppDbContext _context;

        public MesaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Mesa>> ObtenerTodasAsync()
            => await _context.Mesas.OrderBy(m => m.Numero).ToListAsync();

        public async Task<Mesa?> ObtenerPorIdAsync(int id)
            => await _context.Mesas.FindAsync(id);

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}
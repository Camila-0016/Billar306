using Billar306.API.Data;
using Billar306.Data.Models.Clientes;
using Microsoft.EntityFrameworkCore;

namespace Billar306.API.Repositories
{
    public class AbonoFiadoRepository : IAbonoFiadoRepository
    {
        private readonly AppDbContext _context;

        public AbonoFiadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AbonoFiado>> ObtenerPorFiadoAsync(int fiadoId)
            => await _context.AbonosFiado
                .Include(a => a.Usuario)
                .Where(a => a.FiadoId == fiadoId)
                .OrderBy(a => a.Fecha)
                .ToListAsync();

        public async Task AgregarAsync(AbonoFiado abono)
            => await _context.AbonosFiado.AddAsync(abono);

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}
using Billar306.API.Data;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Venta.Mesas;
using Microsoft.EntityFrameworkCore;

namespace Billar306.Persistencia.Repositories
{
    public class MesaRepository : Repository<Mesa>, IMesaRepository
    {
        public MesaRepository(AppDbContext context) : base(context) { }

        public async Task<Mesa?> ObtenerPorNumeroAsync(int numero)
            => await _dbSet.FirstOrDefaultAsync(m => m.Numero == numero);

        public async Task<bool> IntentarOcuparAsync(int mesaId)
        {
            var filasAfectadas = await _context.Mesas
                .Where(m => m.Id == mesaId && !m.Ocupada)
                .ExecuteUpdateAsync(s => s.SetProperty(m => m.Ocupada, true));

            return filasAfectadas == 1;
        }
    }
}
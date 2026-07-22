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
    }
}
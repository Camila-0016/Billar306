using Billar306.API.Data;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Clientes;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Billar306.Persistencia.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cliente>> BuscarPorNombreAsync(string nombre)
        {
            return await _dbSet
                .Where(c => c.NombreCompleto.ToLower().Contains(nombre.ToLower()))
                .ToListAsync();
        }

        public async Task<Cliente?> BuscarExactoAsync(string nombre)
            => await _dbSet.FirstOrDefaultAsync(c => c.NombreCompleto.ToLower() == nombre.ToLower());
    }
}
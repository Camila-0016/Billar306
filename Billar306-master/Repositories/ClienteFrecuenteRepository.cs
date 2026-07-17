using Billar306.API.Data;
using Billar306.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Billar306.API.Repositories
{
    public class ClienteFrecuenteRepository : IClienteFrecuenteRepository
    {
        private readonly AppDbContext _context;

        public ClienteFrecuenteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteFrecuente>> ObtenerTodosActivosAsync()
            => await _context.ClientesFrecuentes
                .Where(c => c.Activo)
                .OrderBy(c => c.NombreCompleto)
                .ToListAsync();

        public async Task<ClienteFrecuente?> ObtenerPorIdAsync(int id)
            => await _context.ClientesFrecuentes.FindAsync(id);

        public async Task<ClienteFrecuente?> BuscarPorNombreAsync(string nombre)
            => await _context.ClientesFrecuentes
                .FirstOrDefaultAsync(c => c.NombreCompleto.ToLower() == nombre.ToLower());

        public async Task AgregarAsync(ClienteFrecuente cliente)
            => await _context.ClientesFrecuentes.AddAsync(cliente);

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}
using Billar306.API.Data;
using Billar306.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Billar306.API.Repositories
{
    public class FiadoRepository : IFiadoRepository
    {
        private readonly AppDbContext _context;

        public FiadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Fiado?> ObtenerPorIdAsync(int id)
            => await _context.Fiados
                .Include(f => f.ClienteFrecuente)
                .Include(f => f.UsuarioRegistro)
                .FirstOrDefaultAsync(f => f.Id == id);

        public async Task<List<Fiado>> ObtenerTodosAsync()
            => await _context.Fiados
                .Include(f => f.ClienteFrecuente)
                .Include(f => f.UsuarioRegistro)
                .OrderByDescending(f => f.FechaRegistro)
                .ToListAsync();

        public async Task<List<Fiado>> ObtenerPorClienteAsync(int clienteId)
            => await _context.Fiados
                .Where(f => f.ClienteFrecuenteId == clienteId)
                .OrderByDescending(f => f.FechaRegistro)
                .ToListAsync();

        public async Task<bool> TieneClienteFiadoActivoAsync(int clienteId)
            => await _context.Fiados
                .AnyAsync(f => f.ClienteFrecuenteId == clienteId && f.Estado == "Pendiente");

        public async Task<bool> TieneClienteFiadoVencidoAsync(int clienteId)
            => await _context.Fiados
                .AnyAsync(f => f.ClienteFrecuenteId == clienteId
                    && f.Estado == "Pendiente"
                    && f.FechaVencimiento < DateTime.Now);

        public async Task AgregarAsync(Fiado fiado)
            => await _context.Fiados.AddAsync(fiado);

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}
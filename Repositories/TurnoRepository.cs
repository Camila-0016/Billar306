using Billar306.API.Data;
using Billar306.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Billar306.API.Repositories
{
    public class TurnoRepository : ITurnoRepository
    {
        private readonly AppDbContext _context;

        public TurnoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Turno?> ObtenerTurnoAbiertoDeUsuarioAsync(int usuarioId)
            => await _context.Turnos
                .FirstOrDefaultAsync(t => t.UsuarioId == usuarioId && t.Estado == "Abierto");

        public async Task<Turno?> ObtenerPorIdAsync(int id)
            => await _context.Turnos.Include(t => t.Usuario).FirstOrDefaultAsync(t => t.Id == id);

        public async Task<List<Turno>> ObtenerTodosAsync()
            => await _context.Turnos.Include(t => t.Usuario).OrderByDescending(t => t.FechaApertura).ToListAsync();

        public async Task AgregarAsync(Turno turno)
            => await _context.Turnos.AddAsync(turno);

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}
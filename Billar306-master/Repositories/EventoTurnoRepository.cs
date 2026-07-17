using Billar306.API.Data;
using Billar306.Data.Models.Operatividad;
using Microsoft.EntityFrameworkCore;

namespace Billar306.API.Repositories
{
    public class EventoTurnoRepository : IEventoTurnoRepository
    {
        private readonly AppDbContext _context;

        public EventoTurnoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventoTurno>> ObtenerTodosAsync()
            => await _context.EventosTurno
                .Include(e => e.Usuario)
                .Include(e => e.Turno)
                .OrderByDescending(e => e.Timestamp)
                .ToListAsync();

        public async Task<List<EventoTurno>> ObtenerPorTurnoAsync(int turnoId)
            => await _context.EventosTurno
                .Include(e => e.Usuario)
                .Where(e => e.TurnoId == turnoId)
                .OrderByDescending(e => e.Timestamp)
                .ToListAsync();

        public async Task<List<EventoTurno>> ObtenerPorUsuarioAsync(int usuarioId)
            => await _context.EventosTurno
                .Include(e => e.Turno)
                .Where(e => e.UsuarioId == usuarioId)
                .OrderByDescending(e => e.Timestamp)
                .ToListAsync();

        public async Task<List<EventoTurno>> ObtenerPorGravedadAsync(string gravedad)
            => await _context.EventosTurno
                .Include(e => e.Usuario)
                .Where(e => e.Gravedad == gravedad && !e.Revisado)
                .OrderByDescending(e => e.Timestamp)
                .ToListAsync();

        public async Task<EventoTurno?> ObtenerPorIdAsync(int id)
            => await _context.EventosTurno
                .Include(e => e.Usuario)
                .Include(e => e.Turno)
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task AgregarAsync(EventoTurno evento)
            => await _context.EventosTurno.AddAsync(evento);

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}
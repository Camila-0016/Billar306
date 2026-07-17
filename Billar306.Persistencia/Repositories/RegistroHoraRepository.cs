using Billar306.API.Data;
using Billar306.Data.Models.Empleado;
using Microsoft.EntityFrameworkCore;

namespace Billar306.API.Repositories
{
    public class RegistroHoraRepository : IRegistroHoraRepository
    {
        private readonly AppDbContext _context;

        public RegistroHoraRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RegistroHoraEmpleado?> ObtenerRegistroAbiertoAsync(int usuarioId, int turnoId)
            => await _context.RegistrosHora
                .FirstOrDefaultAsync(r => r.UsuarioId == usuarioId
                    && r.TurnoId == turnoId
                    && r.Salida == null);

        public async Task<List<RegistroHoraEmpleado>> ObtenerPorUsuarioYPeriodoAsync(
            int usuarioId, DateTime desde, DateTime hasta)
            => await _context.RegistrosHora
                .Include(r => r.Turno)
                .Where(r => r.UsuarioId == usuarioId
                    && r.Entrada >= desde
                    && r.Entrada <= hasta
                    && r.Salida != null)
                .OrderBy(r => r.Entrada)
                .ToListAsync();

        public async Task<List<RegistroHoraEmpleado>> ObtenerPorTurnoAsync(int turnoId)
            => await _context.RegistrosHora
                .Include(r => r.Usuario)
                .Where(r => r.TurnoId == turnoId)
                .ToListAsync();

        public async Task AgregarAsync(RegistroHoraEmpleado registro)
            => await _context.RegistrosHora.AddAsync(registro);

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}
using Billar306.API.Data;
using Billar306.Data.Models.Venta.Mesa;
using Microsoft.EntityFrameworkCore;

namespace Billar306.API.Repositories
{
    public class SesionMesaRepository : ISesionMesaRepository
    {
        private readonly AppDbContext _context;

        public SesionMesaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SesionMesa?> ObtenerSesionAbiertaPorMesaAsync(int mesaId)
            => await _context.SesionesMesa
                .Include(s => s.Consumiciones).ThenInclude(c => c.ItemConfiteria)
                .Include(s => s.ClienteFrecuente)
                .FirstOrDefaultAsync(s => s.MesaId == mesaId && s.Estado == "Abierta");

        public async Task<SesionMesa?> ObtenerPorIdAsync(int id)
            => await _context.SesionesMesa
                .Include(s => s.Consumiciones).ThenInclude(c => c.ItemConfiteria)
                .Include(s => s.ClienteFrecuente)
                .Include(s => s.Fiado)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<List<SesionMesa>> ObtenerPorTurnoAsync(int turnoId)
            => await _context.SesionesMesa
                .Where(s => s.TurnoId == turnoId)
                .Include(s => s.Consumiciones)
                .ToListAsync();

        public async Task AgregarAsync(SesionMesa sesion)
            => await _context.SesionesMesa.AddAsync(sesion);

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
        public async Task<List<SesionMesa>> ObtenerPorFiltroAsync(
    DateTime desde, DateTime hasta, int? mesaId, int? usuarioId)
    => await _context.SesionesMesa
        .Include(s => s.Mesa)
        .Include(s => s.Usuario)
        .Include(s => s.ClienteFrecuente)
        .Include(s => s.Consumiciones).ThenInclude(c => c.ItemConfiteria)
        .Where(s =>
            s.Inicio >= desde &&
            s.Inicio <= hasta &&
            (mesaId == null || s.MesaId == mesaId) &&
            (usuarioId == null || s.UsuarioId == usuarioId) &&
            s.Estado != "Abierta")
        .OrderBy(s => s.MesaId)
        .ThenBy(s => s.Inicio)
        .ToListAsync();
    }
}
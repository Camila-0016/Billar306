using Billar306.API.Data;
using Billar306.Data.Models.Venta;
using Microsoft.EntityFrameworkCore;

namespace Billar306.API.Repositories
{
    public class ItemConfiteriaRepository : IItemConfiteriaRepository
    {
        private readonly AppDbContext _context;

        public ItemConfiteriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ItemConfiteria>> ObtenerTodosActivosAsync()
            => await _context.ItemsConfiteria
                .Where(i => i.Activo)
                .OrderBy(i => i.Nombre)
                .ToListAsync();

        public async Task<ItemConfiteria?> ObtenerPorIdAsync(int id)
            => await _context.ItemsConfiteria.FindAsync(id);

        public async Task AgregarAsync(ItemConfiteria item)
            => await _context.ItemsConfiteria.AddAsync(item);

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}
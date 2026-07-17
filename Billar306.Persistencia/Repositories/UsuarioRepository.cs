using Billar306.API.Data;
using Billar306.Data.Models.Control;
using Microsoft.EntityFrameworkCore;

namespace Billar306.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
            => await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

        public async Task<Usuario?> ObtenerPorIdAsync(int id)
            => await _context.Usuarios.FindAsync(id);

        public async Task<List<Usuario>> ObtenerTodosAsync()
            => await _context.Usuarios.Where(u => u.Activo).ToListAsync();

        public async Task AgregarAsync(Usuario usuario)
            => await _context.Usuarios.AddAsync(usuario);

        public async Task ActualizarAsync(Usuario usuario)
            => _context.Usuarios.Update(usuario);

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}

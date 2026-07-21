using Billar306.API.Data;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Control;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Billar306.Persistencia.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Usuario?> BuscarPorNombreUsuarioAsync(string nombreUsuario)
            => await _dbSet.FirstOrDefaultAsync(u => u.NombreUsuario.ToLower() == nombreUsuario.ToLower());
    }
}
using Billar306.Dominio.Models.Control;
using System.Threading.Tasks;

namespace Billar306.Dominio.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> BuscarPorNombreUsuarioAsync(string nombreUsuario);
    }
}
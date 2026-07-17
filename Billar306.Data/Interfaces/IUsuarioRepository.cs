using Billar306.Data.Models.Control;

namespace Billar306.API.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task<List<Usuario>> ObtenerTodosAsync();
        Task AgregarAsync(Usuario usuario);
        Task ActualizarAsync(Usuario usuario);
        Task GuardarCambiosAsync();
    }
}
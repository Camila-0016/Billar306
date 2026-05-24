using Billar306.API.Models;

namespace Billar306.API.Repositories
{
    public interface IClienteFrecuenteRepository
    {
        Task<List<ClienteFrecuente>> ObtenerTodosActivosAsync();
        Task<ClienteFrecuente?> ObtenerPorIdAsync(int id);
        Task<ClienteFrecuente?> BuscarPorNombreAsync(string nombre);
        Task AgregarAsync(ClienteFrecuente cliente);
        Task GuardarCambiosAsync();
    }
}
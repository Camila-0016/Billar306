using Billar306.API.Models;

namespace Billar306.API.Repositories
{
    public interface IMesaRepository
    {
        Task<List<Mesa>> ObtenerTodasAsync();
        Task<Mesa?> ObtenerPorIdAsync(int id);
        Task GuardarCambiosAsync();
    }
}
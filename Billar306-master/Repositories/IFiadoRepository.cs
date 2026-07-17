using Billar306.API.Models;

namespace Billar306.API.Repositories
{
    public interface IFiadoRepository
    {
        Task<Fiado?> ObtenerPorIdAsync(int id);
        Task<List<Fiado>> ObtenerTodosAsync();
        Task<List<Fiado>> ObtenerPorClienteAsync(int clienteId);
        Task<bool> TieneClienteFiadoActivoAsync(int clienteId);
        Task<bool> TieneClienteFiadoVencidoAsync(int clienteId);
        Task AgregarAsync(Fiado fiado);
        Task GuardarCambiosAsync();
    }
}
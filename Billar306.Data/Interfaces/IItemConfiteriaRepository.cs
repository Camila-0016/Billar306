using Billar306.Data.Models.Venta;

namespace Billar306.API.Repositories
{
    public interface IItemConfiteriaRepository
    {
        Task<List<ItemConfiteria>> ObtenerTodosActivosAsync();
        Task<ItemConfiteria?> ObtenerPorIdAsync(int id);
        Task AgregarAsync(ItemConfiteria item);
        Task GuardarCambiosAsync();
    }
}
using Billar306.API.Models;

namespace Billar306.API.Repositories
{
    public interface IIngresoStockRepository
    {
        Task<List<IngresoStock>> ObtenerPorTurnoAsync(int turnoId);
        Task AgregarAsync(IngresoStock ingreso);
        Task GuardarCambiosAsync();
    }
}
using Billar306.API.Models;

namespace Billar306.API.Repositories
{
    public interface IVentaDirectaRepository
    {
        Task<List<VentaDirecta>> ObtenerPorTurnoAsync(int turnoId);
        Task AgregarAsync(VentaDirecta venta);
        Task GuardarCambiosAsync();
    }
}
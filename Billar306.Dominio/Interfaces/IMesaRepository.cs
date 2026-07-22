using Billar306.Dominio.Models.Venta.Mesas;

namespace Billar306.Dominio.Interfaces
{
    public interface IMesaRepository : IRepository<Mesa>
    {
        Task<Mesa?> ObtenerPorNumeroAsync(int numero);
    }
}
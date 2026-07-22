using Billar306.Dominio.Models.Venta.Mesas;

namespace Billar306.Dominio.Interfaces
{
    public interface ISesionMesaRepository : IRepository<SesionMesa>
    {
        Task<SesionMesa?> ObtenerAbiertaPorMesaAsync(int mesaId);
        Task<IEnumerable<SesionMesa>> ObtenerAbiertasAsync();
    }
}
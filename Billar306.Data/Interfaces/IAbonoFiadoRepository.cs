using Billar306.Data.Models.Clientes;

namespace Billar306.API.Repositories
{
    public interface IAbonoFiadoRepository
    {
        Task<List<AbonoFiado>> ObtenerPorFiadoAsync(int fiadoId);
        Task AgregarAsync(AbonoFiado abono);
        Task GuardarCambiosAsync();
    }
}
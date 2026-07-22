using Billar306.Dominio.Models.Operatividad;

namespace Billar306.Dominio.Interfaces
{
    public interface IDiaLaboralRepository : IRepository<DiaLaboral>
    {
        Task<DiaLaboral?> ObtenerAbiertoActualAsync();
        Task<DiaLaboral?> ObtenerConTurnosAsync(int id);
    }
}
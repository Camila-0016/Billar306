using Billar306.Data.Models.Empleado;

namespace Billar306.API.Repositories
{
    public interface IAnticipoRepository
    {
        Task<List<Anticipo>> ObtenerPorEmpleadoAsync(int empleadoId);
        Task<List<Anticipo>> ObtenerTodosAsync();
        Task<decimal> ObtenerAcumuladoPeriodoAsync(int empleadoId, DateTime desde);
        Task AgregarAsync(Anticipo anticipo);
        Task GuardarCambiosAsync();
    }
}
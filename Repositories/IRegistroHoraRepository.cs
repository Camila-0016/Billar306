using Billar306.API.Models;

namespace Billar306.API.Repositories
{
    public interface IRegistroHoraRepository
    {
        Task<RegistroHoraEmpleado?> ObtenerRegistroAbiertoAsync(int usuarioId, int turnoId);
        Task<List<RegistroHoraEmpleado>> ObtenerPorUsuarioYPeriodoAsync(int usuarioId, DateTime desde, DateTime hasta);
        Task<List<RegistroHoraEmpleado>> ObtenerPorTurnoAsync(int turnoId);
        Task AgregarAsync(RegistroHoraEmpleado registro);
        Task GuardarCambiosAsync();
    }
}
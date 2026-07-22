using Billar306.Dominio.Models.Empleado;

namespace Billar306.Dominio.Interfaces
{
    public interface IRegistroTurnoEmpleadoRepository : IRepository<RegistroTurnoEmpleado>
    {
        Task<RegistroTurnoEmpleado?> ObtenerAbiertoAsync(int turnoId, int empleadoId);
        Task<IEnumerable<RegistroTurnoEmpleado>> ObtenerAbiertosPorTurnoAsync(int turnoId);
        Task<IEnumerable<RegistroTurnoEmpleado>> ObtenerPorTurnoAsync(int turnoId);
        Task<IEnumerable<RegistroTurnoEmpleado>> ObtenerPorEmpleadoAsync(int empleadoId, DateTime? desde, DateTime? hasta);
    }
}
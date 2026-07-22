using Billar306.Dominio.Models.Operatividad;

namespace Billar306.Dominio.Interfaces
{
    public interface ITurnoRepository : IRepository<Turno>
    {
        Task<Turno?> ObtenerTurnoAbiertoAsync();
    }
}
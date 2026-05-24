using Billar306.API.Models;

namespace Billar306.API.Repositories
{
    public interface ITurnoRepository
    {
        Task<Turno?> ObtenerTurnoAbiertoDeUsuarioAsync(int usuarioId);
        Task<Turno?> ObtenerPorIdAsync(int id);
        Task<List<Turno>> ObtenerTodosAsync();
        Task AgregarAsync(Turno turno);
        Task GuardarCambiosAsync();
    }
}

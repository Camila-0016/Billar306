using Billar306.Data.Models.Operatividad;

namespace Billar306.API.Repositories
{
    public interface IEventoTurnoRepository
    {
        Task<List<EventoTurno>> ObtenerTodosAsync();
        Task<List<EventoTurno>> ObtenerPorTurnoAsync(int turnoId);
        Task<List<EventoTurno>> ObtenerPorUsuarioAsync(int usuarioId);
        Task<List<EventoTurno>> ObtenerPorGravedadAsync(string gravedad);
        Task<EventoTurno?> ObtenerPorIdAsync(int id);
        Task AgregarAsync(EventoTurno evento);
        Task GuardarCambiosAsync();
    }
}
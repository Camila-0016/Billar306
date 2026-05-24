using Billar306.API.Models;

namespace Billar306.API.Repositories
{
    public interface ISesionMesaRepository
    {
        Task<SesionMesa?> ObtenerSesionAbiertaPorMesaAsync(int mesaId);
        Task<SesionMesa?> ObtenerPorIdAsync(int id);
        Task<List<SesionMesa>> ObtenerPorTurnoAsync(int turnoId);
        Task AgregarAsync(SesionMesa sesion);
        Task GuardarCambiosAsync();
        Task<List<SesionMesa>> ObtenerPorFiltroAsync(DateTime desde, DateTime hasta, int? mesaId, int? usuarioId);
    }
}

namespace Billar306.Aplicacion.DTOs.Turnos
{
    public record RegistroTurnoEmpleadoDto(
        int Id, int TurnoId, int EmpleadoId,
        DateTime FechaInicio, DateTime? Salida, decimal? HorasTrabajadas
    );
}

namespace Billar306.Aplicacion.DTOs.Turnos
{
    public record TurnoDetalleDto(
        int Id, int TitularId, int? AuxiliarId,
        DateTime FechaInicio, DateTime? Salida,
        List<RegistroTurnoEmpleadoDto> Registros
    );
}
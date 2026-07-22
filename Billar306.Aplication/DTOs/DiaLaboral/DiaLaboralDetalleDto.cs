
namespace Billar306.Aplicacion.DTOs.Turnos
{
    public record DiaLaboralDetalleDto(
        int Id, DateTime FechaInicio, DateTime? FechaCierre, bool EstaCerrado,
        List<TurnoDetalleDto> Turnos
    );
}
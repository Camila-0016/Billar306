
namespace Billar306.Aplicacion.DTOs.Turnos
{
    public record TurnoDto(
        int Id, int DiaLaboralId, int TitularId, int? AuxiliarId,
        DateTime FechaInicio, DateTime? Salida
    );
}
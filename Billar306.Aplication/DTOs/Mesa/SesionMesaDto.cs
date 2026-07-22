namespace Billar306.Aplicacion.DTOs.Mesas
{
    public record SesionMesaDto(
        int Id, int MesaId, int ClienteId, int TurnoId,
        int EmpleadoAperturaId, int? EmpleadoCierreId,
        DateTime FechaInicio, DateTime? FechaFin,
        decimal MontoSesionMesa, decimal Total
    );
}
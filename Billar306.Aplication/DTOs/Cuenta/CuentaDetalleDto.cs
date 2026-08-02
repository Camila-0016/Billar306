namespace Billar306.Aplicacion.DTOs.Cuentas
{
    public record CuentaDetalleDto(
        int Id, int? MesaId, int? NumeroMesa, int ClienteId, int TurnoId,
        int EmpleadoAperturaId, int? EmpleadoCierreId,
        DateTime FechaInicio, DateTime? FechaFin,
        decimal? MontoMesaActual, decimal TotalActual,
        int? VentaConfiteriaId
    );
}
using Billar306.Aplicacion.DTOs.Turnos;

namespace Billar306.Aplicacion.DTOs.Reportes
{
    public record MesaDelTurnoDto(int SesionId, int MesaNumero, string Cliente, decimal Total, bool Cerrada);
    public record VentaDirectaDelTurnoDto(int CuentaId, string Cliente, decimal Total);
    public record ProductoVendidoDto(int ProductoId, string Nombre, int CantidadTotal);
    public record TurnoReporteDto(
        int TurnoId, int TitularId, int? AuxiliarId,
        DateTime FechaInicio, DateTime? Salida,
        List<RegistroTurnoEmpleadoDto> Horas,
        List<MesaDelTurnoDto> Mesas,
        List<VentaDirectaDelTurnoDto> VentasDirectas,
        List<ProductoVendidoDto> ProductosVendidos
    );
}
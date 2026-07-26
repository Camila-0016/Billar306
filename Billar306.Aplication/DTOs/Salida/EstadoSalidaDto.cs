namespace Billar306.Aplicacion.DTOs.Salida
{
    public record EstadoSalidaDto(int TurnoId, bool EsUnicoActivo, bool HayMesasAbiertas);
    public record ConfirmarSalidaDto(int EmpleadoId, bool CerrarDiaLaboral);
}
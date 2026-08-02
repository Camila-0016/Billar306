namespace Billar306.Aplicacion.DTOs.Mesas
{
    public record AbrirSesionMesaDto(int MesaId, int? ClienteId, string? NombreClienteNuevo);
}
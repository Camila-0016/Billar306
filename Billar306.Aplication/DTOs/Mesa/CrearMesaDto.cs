using System.ComponentModel.DataAnnotations;

namespace Billar306.Aplicacion.DTOs.Mesas
{
    public record CrearMesaDto([Range(1, int.MaxValue)] int Numero);
}
using Billar306.Dominio.Models.Control;

namespace Billar306.Aplicacion.DTOs.Configuraciones
{
    public record ConfiguracionSistemaDto(int Id, TipoParametro Clave, decimal Valor, string? Descripcion);
}
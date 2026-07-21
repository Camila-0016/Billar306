using Billar306.Dominio.Models;

namespace Billar306.Dominio.Models.Control
{
    public class ConfiguracionSistema : EntidadBase
    {
        // Usamos una enumeración para evitar errores de tipeo al buscar parámetros
        public TipoParametro Clave { get; set; }

        public decimal Valor { get; set; }
        public string? Descripcion { get; set; }
    }
}
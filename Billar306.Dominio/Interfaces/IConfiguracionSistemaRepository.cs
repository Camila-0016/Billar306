using Billar306.Dominio.Models.Control;

namespace Billar306.Dominio.Interfaces
{
    public interface IConfiguracionSistemaRepository : IRepository<ConfiguracionSistema>
    {
        Task<ConfiguracionSistema?> ObtenerPorClaveAsync(TipoParametro clave);
    }
}
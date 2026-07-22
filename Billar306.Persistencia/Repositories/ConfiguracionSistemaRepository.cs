using Billar306.API.Data;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Control;
using Microsoft.EntityFrameworkCore;

namespace Billar306.Persistencia.Repositories
{
    public class ConfiguracionSistemaRepository : Repository<ConfiguracionSistema>, IConfiguracionSistemaRepository
    {
        public ConfiguracionSistemaRepository(AppDbContext context) : base(context) { }

        public async Task<ConfiguracionSistema?> ObtenerPorClaveAsync(TipoParametro clave)
            => await _dbSet.FirstOrDefaultAsync(c => c.Clave == clave);
    }
}
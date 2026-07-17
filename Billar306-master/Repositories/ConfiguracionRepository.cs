using Billar306.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Billar306.API.Repositories
{
    public class ConfiguracionRepository : IConfiguracionRepository
    {
        private readonly AppDbContext _context;

        public ConfiguracionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string?> ObtenerValorAsync(string clave)
        {
            var config = await _context.ConfiguracionSistema
                .FirstOrDefaultAsync(c => c.Clave == clave);
            return config?.Valor;
        }

        public async Task<decimal> ObtenerDecimalAsync(string clave, decimal valorDefecto = 0)
        {
            var valor = await ObtenerValorAsync(clave);
            return decimal.TryParse(valor, out var resultado) ? resultado : valorDefecto;
        }

        public async Task<int> ObtenerEnteroAsync(string clave, int valorDefecto = 0)
        {
            var valor = await ObtenerValorAsync(clave);
            return int.TryParse(valor, out var resultado) ? resultado : valorDefecto;
        }

        public async Task ActualizarValorAsync(string clave, string nuevoValor)
        {
            var config = await _context.ConfiguracionSistema
                .FirstOrDefaultAsync(c => c.Clave == clave);
            if (config != null)
                config.Valor = nuevoValor;
        }

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}
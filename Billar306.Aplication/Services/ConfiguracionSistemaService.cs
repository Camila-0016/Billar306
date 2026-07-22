using Billar306.Aplicacion.DTOs.Configuraciones;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Control;

namespace Billar306.Aplicacion.Services
{
    public class ConfiguracionSistemaService
    {
        private readonly IConfiguracionSistemaRepository _configRepository;

        public ConfiguracionSistemaService(IConfiguracionSistemaRepository configRepository)
        {
            _configRepository = configRepository;
        }

        public async Task<List<ConfiguracionSistemaDto>> ObtenerTodasAsync()
        {
            var configs = await _configRepository.ObtenerTodosAsync();
            return configs.Select(MapearADto).ToList();
        }

        public async Task<ConfiguracionSistemaDto?> ObtenerPorClaveAsync(TipoParametro clave)
        {
            var config = await _configRepository.ObtenerPorClaveAsync(clave);
            return config is null ? null : MapearADto(config);
        }

        public async Task<ConfiguracionSistemaDto> EstablecerAsync(TipoParametro clave, EstablecerConfiguracionDto dto)
        {
            var existente = await _configRepository.ObtenerPorClaveAsync(clave);

            if (existente is null)
            {
                var nueva = new ConfiguracionSistema
                {
                    Clave = clave,
                    Valor = dto.Valor,
                    Descripcion = dto.Descripcion
                };
                await _configRepository.AgregarAsync(nueva);
                return MapearADto(nueva);
            }

            existente.Valor = dto.Valor;
            existente.Descripcion = dto.Descripcion;
            await _configRepository.ActualizarAsync(existente);
            return MapearADto(existente);
        }

        private static ConfiguracionSistemaDto MapearADto(ConfiguracionSistema c)
            => new(c.Id, c.Clave, c.Valor, c.Descripcion);
    }
}
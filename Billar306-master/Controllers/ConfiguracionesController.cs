using Billar306.Aplicacion.DTOs.Configuraciones;
using Billar306.Aplicacion.Services;
using Billar306.Dominio.Models.Control;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracionesController : ControllerBase
    {
        private readonly ConfiguracionSistemaService _configService;

        public ConfiguracionesController(ConfiguracionSistemaService configService)
        {
            _configService = configService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ConfiguracionSistemaDto>>> ObtenerTodas()
            => Ok(await _configService.ObtenerTodasAsync());

        [HttpGet("{clave}")]
        public async Task<ActionResult<ConfiguracionSistemaDto>> ObtenerPorClave(TipoParametro clave)
        {
            var config = await _configService.ObtenerPorClaveAsync(clave);
            if (config is null) return NotFound(new { mensaje = "Ese parámetro todavía no fue configurado" });
            return Ok(config);
        }

        // si no existe lo crea, si existe lo actualiza
        [HttpPut("{clave}")]
        public async Task<ActionResult<ConfiguracionSistemaDto>> Establecer(TipoParametro clave, [FromBody] EstablecerConfiguracionDto dto)
        {
            var config = await _configService.EstablecerAsync(clave, dto);
            return Ok(config);
        }
    }
}
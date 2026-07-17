using Billar306.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "jefe")]
    public class ConfiguracionController : ControllerBase
    {
        private readonly IConfiguracionRepository _configRepo;

        public ConfiguracionController(IConfiguracionRepository configRepo)
        {
            _configRepo = configRepo;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            // Devuelve todas las claves visibles para el jefe
            var claves = new[]
            {
                "TarifaHoraMesa", "RecargoPorcentajeNocturno",
                "HoraInicioRecargo", "HoraFinRecargo",
                "TarifaHoraEmpleado", "TarifaHoraEncargado",
                "LimiteAnticipoPorc", "MontoMaxGravedadBaja",
                "PeriodoRedondeo", "DuracionTokenHoras"
            };

            var resultado = new List<object>();
            foreach (var clave in claves)
            {
                var valor = await _configRepo.ObtenerValorAsync(clave);
                resultado.Add(new { clave, valor });
            }

            return Ok(resultado);
        }

        [HttpPatch("{clave}")]
        public async Task<IActionResult> Actualizar(string clave, [FromBody] ActualizarConfigRequest request)
        {
            await _configRepo.ActualizarValorAsync(clave, request.Valor);
            await _configRepo.GuardarCambiosAsync();
            return Ok(new { mensaje = $"Configuración '{clave}' actualizada a '{request.Valor}'." });
        }
    }

    public class ActualizarConfigRequest
    {
        public string Valor { get; set; } = string.Empty;
    }
}
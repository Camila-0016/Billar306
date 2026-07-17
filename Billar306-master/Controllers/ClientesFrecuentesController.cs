using Billar306.API.DTOs;
using Billar306.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientesFrecuentesController : ControllerBase
    {
        private readonly ClienteFrecuenteService _clienteService;

        public ClientesFrecuentesController(ClienteFrecuenteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var clientes = await _clienteService.ObtenerTodosAsync();
            var resultado = clientes.Select(c => new ClienteFrecuenteDto
            {
                Id = c.Id,
                NombreCompleto = c.NombreCompleto,
                Telefono = c.Telefono
            });
            return Ok(resultado);
        }

        [HttpGet("{id}/resumen")]
        [Authorize(Roles = "encargado,jefe")]
        public async Task<IActionResult> ObtenerResumen(int id)
        {
            var resumen = await _clienteService.ObtenerResumenAsync(id);
            if (resumen == null) return NotFound(new { mensaje = "Cliente no encontrado." });
            return Ok(resumen);
        }

        [HttpPost]
        [Authorize(Roles = "encargado,jefe")]
        public async Task<IActionResult> Crear([FromBody] CrearClienteRequest request)
        {
            var (ok, error, esduplicado, existente) =
                await _clienteService.CrearClienteAsync(request.NombreCompleto, request.Telefono);

            if (!ok && esduplicado)
                return Conflict(new
                {
                    mensaje = error,
                    clienteExistente = new ClienteFrecuenteDto
                    {
                        Id = existente!.Id,
                        NombreCompleto = existente.NombreCompleto,
                        Telefono = existente.Telefono
                    }
                });

            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Cliente creado correctamente." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "encargado,jefe")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var (ok, error) = await _clienteService.DesactivarAsync(id);
            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Cliente desactivado." });
        }
    }

    public class CrearClienteRequest
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Telefono { get; set; }
    }
}
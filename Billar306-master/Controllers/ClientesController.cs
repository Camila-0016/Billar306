using Billar306.Aplicacion.DTOs.Clientes;
using Billar306.Aplicacion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteDto>>> ObtenerTodos()
        {
            var clientes = await _clienteService.ObtenerTodosAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> ObtenerPorId(int id)
        {
            var cliente = await _clienteService.ObtenerPorIdAsync(id);
            if (cliente is null) return NotFound(new { mensaje = "Cliente no encontrado" });
            return Ok(cliente);
        }

        [HttpGet("buscar/{nombre}")]
        public async Task<ActionResult<List<ClienteDto>>> BuscarPorNombre(string nombre)
        {
            var clientes = await _clienteService.BuscarPorNombreAsync(nombre);
            if (clientes.Count == 0)
                return NotFound(new { mensaje = "No hay clientes con ese nombre" });
            return Ok(clientes);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> Crear([FromBody] CrearClienteDto dto)
        {
            var (exito, error, nuevoCliente) = await _clienteService.AgregarAsync(dto);
            if (!exito) return Conflict(new { mensaje = error });
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoCliente!.Id }, nuevoCliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarClienteDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { mensaje = "Los ID no coinciden (el ID de la ruta es distinto al del body)" });

            var actualizado = await _clienteService.ActualizarAsync(id, dto);
            if (!actualizado) return NotFound(new { mensaje = "Cliente no encontrado" });
            return NoContent();
        }
    }
}
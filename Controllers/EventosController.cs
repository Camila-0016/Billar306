using Billar306.API.DTOs;
using Billar306.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Billar306.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "encargado,jefe")]
    public class EventosController : ControllerBase
    {
        private readonly EventoService _eventoService;

        public EventosController(EventoService eventoService)
        {
            _eventoService = eventoService;
        }
        
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos(
            [FromQuery] int pagina = 1,
            [FromQuery] int porPagina = 20)
        {
            var eventos = await _eventoService.ObtenerTodosAsync();
            var total = eventos.Count;
            var paginados = eventos
                .Skip((pagina - 1) * porPagina)
                .Take(porPagina);

            return Ok(new
            {
                TotalRegistros = total,
                PaginaActual = pagina,
                TotalPaginas = (int)Math.Ceiling((double)total / porPagina),
                Detalle = paginados.Select(e => new EventoDto
                {
                    Id = e.Id,
                    TipoEvento = e.TipoEvento,
                    Gravedad = e.Gravedad,
                    Descripcion = e.Descripcion,
                    Timestamp = e.Timestamp,
                    Revisado = e.Revisado,
                    NotaRevision = e.NotaRevision,
                    Usuario = e.Usuario?.NombreCompleto
                })
            });
        }

        [HttpGet("alertas")]
        public async Task<IActionResult> ObtenerAlertas()
        {
            var alertas = await _eventoService.ObtenerAlertasAltaAsync();
            var resultado = alertas.Select(e => new EventoDto
            {
                Id = e.Id,
                TipoEvento = e.TipoEvento,
                Descripcion = e.Descripcion,
                Timestamp = e.Timestamp,
                Usuario = e.Usuario?.NombreCompleto
            });
            return Ok(resultado);
        }

        [HttpGet("empleado/{usuarioId}")]
        public async Task<IActionResult> ObtenerPorUsuario(int usuarioId)
        {
            var eventos = await _eventoService.ObtenerPorUsuarioAsync(usuarioId);
            var resultado = eventos.Select(e => new EventoDto
            {
                Id = e.Id,
                TipoEvento = e.TipoEvento,
                Gravedad = e.Gravedad,
                Descripcion = e.Descripcion,
                Timestamp = e.Timestamp,
                Revisado = e.Revisado
            });
            return Ok(resultado);
        }

        [HttpPost("{id}/revisar")]
        public async Task<IActionResult> MarcarRevisado(int id, [FromBody] RevisarEventoRequest request)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var rol = User.FindFirstValue(ClaimTypes.Role)!;

            var (ok, error) = await _eventoService.MarcarRevisadoAsync(id, usuarioId, rol, request.Nota);
            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Evento marcado como revisado." });
        }
    }

    public class RevisarEventoRequest
    {
        public string Nota { get; set; } = string.Empty;
    }
}
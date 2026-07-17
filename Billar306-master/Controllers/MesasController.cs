using Billar306.API.DTOs;
using Billar306.API.Repositories;
using Billar306.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Billar306.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MesasController : ControllerBase
    {
        private readonly MesaService _mesaService;
        private readonly TurnoService _turnoService;
        private readonly ISesionMesaRepository _sesionMesaRepo;
        public MesasController(MesaService mesaService, TurnoService turnoService, ISesionMesaRepository sesionMesaRepo)
        {
            _mesaService = mesaService;
            _turnoService = turnoService;
            _sesionMesaRepo = sesionMesaRepo;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var mesas = await _mesaService.ObtenerTodasAsync();
            var resultado = mesas.Select(m => new MesaDto
            {
                Id = m.Id,
                Numero = m.Numero,
                Estado = m.Estado
            });
            return Ok(resultado);
        }

        [HttpPost("{id}/abrir")]
        public async Task<IActionResult> Abrir(int id, [FromBody] AbrirMesaRequest request)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var turno = await _turnoService.ObtenerTurnoAbiertoAsync(usuarioId);
            if (turno == null)
                return BadRequest(new { mensaje = "No tenés un turno abierto. Abrí el turno antes de operar mesas." });

            var (ok, error) = await _mesaService.AbrirMesaAsync(id, usuarioId, turno.Id, request.ClienteFrecuenteId);
            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Mesa abierta correctamente." });
        }

        [HttpPost("{id}/cobrar")]
        public async Task<IActionResult> Cobrar(int id, [FromBody] CobrarMesaRequest request)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var rol = User.FindFirstValue(ClaimTypes.Role)!;

            var (ok, error, total) = await _mesaService.CerrarMesaAsync(
                id, usuarioId, request.MontoRecibido,
                request.ClienteFrecuenteId, request.Prenda,
                request.DescripcionPrenda, request.FechaVencimiento);

            if (!ok) return BadRequest(new { mensaje = error, totalRequerido = total });
            return Ok(new { mensaje = "Mesa cerrada.", total });
        }

        [HttpGet("historial")]
        [Authorize(Roles = "encargado,jefe")]
        public async Task<IActionResult> ObtenerHistorial(
    [FromQuery] DateTime desde,
    [FromQuery] DateTime hasta,
    [FromQuery] int? mesaId,
    [FromQuery] int? usuarioId,
    [FromQuery] int pagina = 1,
    [FromQuery] int porPagina = 20)
        {
            var sesiones = await _sesionMesaRepo.ObtenerPorFiltroAsync(desde, hasta, mesaId, usuarioId);

            var total = sesiones.Count;
            var paginadas = sesiones
                .Skip((pagina - 1) * porPagina)
                .Take(porPagina)
                .ToList();

            var detalle = paginadas.Select(s => new
            {
                Mesa = s.Mesa?.Numero,
                Empleado = s.Usuario?.NombreCompleto,
                Cliente = s.ClienteFrecuente?.NombreCompleto ?? "Sin identificar",
                s.Inicio,
                s.Fin,
                DuracionMinutos = s.Fin.HasValue ? (int)(s.Fin.Value - s.Inicio).TotalMinutes : 0,
                s.Estado,
                s.TotalCobrado,
                s.MontoRecibido,
                Consumiciones = s.Consumiciones.Select(c => new
                {
                    Producto = c.ItemConfiteria?.Nombre,
                    c.Cantidad,
                    c.PrecioUnitario
                })
            });

            return Ok(new
            {
                TotalRegistros = total,
                PaginaActual = pagina,
                PorPagina = porPagina,
                TotalPaginas = (int)Math.Ceiling((double)total / porPagina),
                TotalCobrado = sesiones.Where(s => s.Estado == "Cobrada").Sum(s => s.TotalCobrado ?? 0),
                TotalFiados = sesiones.Where(s => s.Estado == "Fiada").Sum(s => s.TotalCobrado ?? 0),
                Detalle = detalle
            });
        }
    }

    public class AbrirMesaRequest
    {
        public int? ClienteFrecuenteId { get; set; }
    }

    public class CobrarMesaRequest
    {
        public decimal MontoRecibido { get; set; }
        public int? ClienteFrecuenteId { get; set; }
        public string? Prenda { get; set; }
        public string? DescripcionPrenda { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }
}
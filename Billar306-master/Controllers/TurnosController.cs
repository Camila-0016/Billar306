using Billar306.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Billar306.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TurnosController : ControllerBase
    {
        private readonly TurnoService _turnoService;
        private readonly EventoService _eventoService;

        public TurnosController(TurnoService turnoService, EventoService eventoService)
        {
            _turnoService = turnoService;
            _eventoService = eventoService;
        }

        [HttpPost("abrir")]
        public async Task<IActionResult> Abrir([FromBody] AbrirTurnoRequest request)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var conteo = request.ConteoStock
                .Select(c => (c.ItemId, c.StockContado))
                .ToList();

            var (ok, error, turno) = await _turnoService.AbrirTurnoAsync(usuarioId, conteo);
            if (!ok) return BadRequest(new { mensaje = error });

            return Ok(new { mensaje = "Turno abierto.", turnoId = turno!.Id });
        }

        [HttpPost("{id}/ingreso-stock")]
        public async Task<IActionResult> RegistrarIngreso(int id, [FromBody] IngresoStockRequest request)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var (ok, error) = await _turnoService.RegistrarIngresoStockAsync(
                id, usuarioId, request.ItemId, request.Cantidad, request.Nota);

            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Ingreso de stock registrado." });
        }

        [HttpGet("{id}/ingresos-stock")]
        public async Task<IActionResult> ObtenerIngresos(int id)
        {
            var ingresos = await _turnoService.ObtenerIngresosDelTurnoAsync(id);
            return Ok(ingresos.Select(i => new
            {
                i.Id,
                Producto = i.ItemConfiteria?.Nombre,
                i.Cantidad,
                i.Timestamp,
                i.Nota,
                RegistradoPor = i.Usuario?.NombreCompleto
            }));
        }

        [HttpPost("{id}/cerrar")]
        public async Task<IActionResult> Cerrar(int id, [FromBody] CerrarTurnoRequest request)
        {
            var (ok, error) = await _turnoService.CerrarTurnoAsync(
                id, request.EfectivoConfiteria, request.EfectivoMaquinas, request.Nota);
            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Turno cerrado correctamente." });
        }

        [HttpGet("abierto")]
        public async Task<IActionResult> ObtenerAbierto()
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var turno = await _turnoService.ObtenerTurnoAbiertoAsync(usuarioId);
            if (turno == null)
                return NotFound(new { mensaje = "No tenés un turno abierto." });

            return Ok(new
            {
                turno.Id,
                turno.FechaApertura,
                turno.Estado
            });
        }
    }

    public class AbrirTurnoRequest
    {
        public List<ConteoStockRequest> ConteoStock { get; set; } = new();
    }

    public class ConteoStockRequest
    {
        public int ItemId { get; set; }
        public int StockContado { get; set; }
    }

    public class IngresoStockRequest
    {
        public int ItemId { get; set; }
        public int Cantidad { get; set; }
        public string? Nota { get; set; }
    }

    public class CerrarTurnoRequest
    {
        public decimal EfectivoConfiteria { get; set; }
        public decimal EfectivoMaquinas { get; set; }
        public string? Nota { get; set; }
    }
}
using System.Security.Claims;
using Billar306.API.DTOs;
using Billar306.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ConfiteriaController : ControllerBase
    {
        private readonly ConfiteriaService _confiteriaService;
        private readonly TurnoService _turnoService;

        public ConfiteriaController(ConfiteriaService confiteriaService, TurnoService turnoService)
        {
            _confiteriaService = confiteriaService;
            _turnoService = turnoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var items = await _confiteriaService.ObtenerTodosAsync();
            var resultado = items.Select(i => new ItemConfiteriaDto
            {
                Id = i.Id,
                Nombre = i.Nombre,
                Precio = i.Precio,
                StockActual = i.StockActual,
                StockMinimo = i.StockMinimo,
                SinStock = i.StockActual == 0,
                StockBajo = i.StockActual <= i.StockMinimo
            });
            return Ok(resultado);
        }

        [HttpPost]
        [Authorize(Roles = "jefe")]
        public async Task<IActionResult> Crear([FromBody] CrearItemRequest request)
        {
            var (ok, error) = await _confiteriaService.CrearItemAsync(
                request.Nombre, request.Precio, request.StockInicial, request.StockMinimo);

            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Ítem creado correctamente." });
        }

        [HttpPost("{mesaId}/agregar-item/{itemId}")]
        public async Task<IActionResult> AgregarItemAMesa(int mesaId, int itemId)
        {
            var (ok, error, stockBajo) = await _confiteriaService.AgregarItemAMesaAsync(mesaId, itemId);
            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Ítem agregado a la mesa.", alertaStockBajo = stockBajo });
        }

        [HttpPatch("{itemId}/precio")]
        [Authorize(Roles = "jefe")]
        public async Task<IActionResult> ActualizarPrecio(int itemId, [FromBody] ActualizarPrecioRequest request)
        {
            var (ok, error) = await _confiteriaService.ActualizarPrecioAsync(itemId, request.Precio);
            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Precio actualizado." });
        }

        [HttpPatch("{itemId}/stock")]
        [Authorize(Roles = "jefe")]
        public async Task<IActionResult> ActualizarStock(int itemId, [FromBody] ActualizarStockRequest request)
        {
            var (ok, error) = await _confiteriaService.ActualizarStockAsync(itemId, request.Stock);
            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Stock actualizado." });
        }

        [HttpPost("venta-directa")]
        public async Task<IActionResult> VenderDirecto([FromBody] VentaDirectaRequest request)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var turno = await _turnoService.ObtenerTurnoAbiertoAsync(usuarioId);
            if (turno == null)
                return BadRequest(new { mensaje = "No hay turno abierto." });

            var (ok, error) = await _confiteriaService.VenderDirectoAsync(
                turno.Id, usuarioId, request.ItemId, request.Cantidad);

            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Venta registrada." });
        }

        [HttpGet("ventas-turno/{turnoId}")]
        [Authorize(Roles = "encargado,jefe")]
        public async Task<IActionResult> ObtenerVentasTurno(int turnoId)
        {
            var ventas = await _confiteriaService.ObtenerVentasDelTurnoAsync(turnoId);
            return Ok(ventas.Select(v => new
            {
                v.Id,
                Producto = v.ItemConfiteria?.Nombre,
                v.Cantidad,
                v.PrecioUnitario,
                v.Total,
                v.Timestamp,
                Vendidopor = v.Usuario?.NombreCompleto
            }));
        }

        public class VentaDirectaRequest
        {
            public int ItemId { get; set; }
            public int Cantidad { get; set; } = 1;
        }
    }

    public class CrearItemRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int StockInicial { get; set; }
        public int StockMinimo { get; set; } = 5;
    }

    public class ActualizarPrecioRequest
    {
        public decimal Precio { get; set; }
    }

    public class ActualizarStockRequest
    {
        public int Stock { get; set; }
    }
}
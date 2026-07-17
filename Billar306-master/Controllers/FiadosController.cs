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
    public class FiadosController : ControllerBase
    {
        private readonly FiadoService _fiadoService;
        private readonly IAbonoFiadoRepository _abonoRepo;

        public FiadosController(FiadoService fiadoService, IAbonoFiadoRepository abonoRepo)
        {
            _fiadoService = fiadoService;
            _abonoRepo = abonoRepo;
        }

        [HttpGet]
        [Authorize(Roles = "encargado,jefe")]
        public async Task<IActionResult> ObtenerTodos(
    [FromQuery] int pagina = 1,
    [FromQuery] int porPagina = 20)
        {
            await _fiadoService.ActualizarFiadosVencidosAsync();
            var fiados = await _fiadoService.ObtenerTodosAsync();
            var total = fiados.Count;
            var paginados = fiados
                .Skip((pagina - 1) * porPagina)
                .Take(porPagina);

            return Ok(new
            {
                TotalRegistros = total,
                PaginaActual = pagina,
                TotalPaginas = (int)Math.Ceiling((double)total / porPagina),
                Detalle = paginados.Select(f => new FiadoDto
                {
                    Id = f.Id,
                    MontoTotal = f.MontoTotal,
                    Prenda = f.Prenda,
                    DescripcionPrenda = f.DescripcionPrenda,
                    FechaRegistro = f.FechaRegistro,
                    FechaVencimiento = f.FechaVencimiento,
                    FechaPago = f.FechaPago,
                    Estado = f.Estado,
                    Cliente = f.ClienteFrecuente?.NombreCompleto,
                    RegistradoPor = f.UsuarioRegistro?.NombreCompleto
                })
            });
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarFiadoRequest request)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var (ok, error) = await _fiadoService.RegistrarFiadoAsync(
                request.MesaId, request.ClienteFrecuenteId, usuarioId,
                request.Prenda, request.DescripcionPrenda, request.FechaVencimiento);

            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Fiado registrado correctamente." });
        }

        [HttpPost("{id}/cobrar")]
        [Authorize]
        public async Task<IActionResult> Cobrar(int id)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var rol = User.FindFirstValue(ClaimTypes.Role)!;
            var (ok, error) = await _fiadoService.CobrarFiadoAsync(id, usuarioId);
            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Fiado cobrado. Prenda liberada." });
        }

        [HttpPost("{id}/abonar")]
        public async Task<IActionResult> Abonar(int id, [FromBody] AbonarFiadoRequest request)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var (ok, error) = await _fiadoService.RegistrarAbonoAsync(id, usuarioId, request.Monto, request.Nota);
            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Abono registrado correctamente." });
        }

        [HttpGet("{id}/abonos")]
        public async Task<IActionResult> ObtenerAbonos(int id)
        {
            var abonos = await _abonoRepo.ObtenerPorFiadoAsync(id);
            return Ok(abonos.Select(a => new
            {
                a.Id,
                a.Monto,
                a.Fecha,
                a.Nota,
                RegistradoPor = a.Usuario?.NombreCompleto
            }));
        }

        [HttpPost("registrar-directo")]
        public async Task<IActionResult> RegistrarDirecto([FromBody] RegistrarFiadoDirectoRequest request)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var (ok, error) = await _fiadoService.RegistrarFiadoDirectoAsync(
                request.ClienteFrecuenteId, usuarioId,
                request.Monto, request.Prenda,
                request.DescripcionPrenda, request.FechaVencimiento);

            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Fiado directo registrado correctamente." });
        }

    }
    public class AbonarFiadoRequest
    {
        public decimal Monto { get; set; }
        public string? Nota { get; set; }
    }
    public class RegistrarFiadoRequest
    {
        public int MesaId { get; set; }
        public int ClienteFrecuenteId { get; set; }
        public string Prenda { get; set; } = string.Empty;
        public string? DescripcionPrenda { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
    public class RegistrarFiadoDirectoRequest
    {
        public int ClienteFrecuenteId { get; set; }
        public decimal Monto { get; set; }
        public string Prenda { get; set; } = string.Empty;
        public string? DescripcionPrenda { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
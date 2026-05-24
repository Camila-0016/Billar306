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
    public class AnticiposController : ControllerBase
    {
        private readonly AnticipoService _anticipoService;
        private readonly TurnoService _turnoService;

        public AnticiposController(AnticipoService anticipoService, TurnoService turnoService)
        {
            _anticipoService = anticipoService;
            _turnoService = turnoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos(
    [FromQuery] int pagina = 1,
    [FromQuery] int porPagina = 20)
        {
            var anticipos = await _anticipoService.ObtenerTodosAsync();
            var total = anticipos.Count;
            var paginados = anticipos
                .Skip((pagina - 1) * porPagina)
                .Take(porPagina);

            return Ok(new
            {
                TotalRegistros = total,
                PaginaActual = pagina,
                TotalPaginas = (int)Math.Ceiling((double)total / porPagina),
                Detalle = paginados.Select(a => new AnticipoDto
                {
                    Id = a.Id,
                    Monto = a.Monto,
                    Fecha = a.Fecha,
                    ForzadoPorJefe = a.ForzadoPorJefe,
                    Empleado = a.Empleado?.NombreCompleto,
                    Autorizante = a.UsuarioAutorizante?.NombreCompleto
                })
            });
        }

        [HttpGet("empleado/{empleadoId}")]
        public async Task<IActionResult> ObtenerPorEmpleado(int empleadoId)
        {
            var anticipos = await _anticipoService.ObtenerPorEmpleadoAsync(empleadoId);
            var resultado = anticipos.Select(a => new AnticipoDto
            {
                Id = a.Id,
                Monto = a.Monto,
                Fecha = a.Fecha,
                ForzadoPorJefe = a.ForzadoPorJefe
            });
            return Ok(resultado);
        }

        [HttpGet("empleado/{empleadoId}/limite")]
        public async Task<IActionResult> VerificarLimite(int empleadoId)
        {
            var (ok, error, requiereJefe, acumulado, limite) =
                await _anticipoService.VerificarLimiteAsync(empleadoId);

            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new
            {
                acumulado,
                limite,
                disponible = limite - acumulado,
                requiereAutorizacionJefe = requiereJefe
            });
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] RegistrarAnticipoRequest request)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var rol = User.FindFirstValue(ClaimTypes.Role)!;

            var turno = await _turnoService.ObtenerTurnoAbiertoAsync(usuarioId);
            if (turno == null)
                return BadRequest(new { mensaje = "No hay turno abierto para registrar el anticipo." });

            var (ok, error) = await _anticipoService.RegistrarAnticipoAsync(
                request.EmpleadoId, turno.Id, usuarioId, request.Monto, rol, request.Forzar);

            if (!ok) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Anticipo registrado correctamente." });
        }
    }

    public class RegistrarAnticipoRequest
    {
        public int EmpleadoId { get; set; }
        public decimal Monto { get; set; }
        public bool Forzar { get; set; } = false;
    }
}
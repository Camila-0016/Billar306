using Billar306.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HealthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                _context.Database.CanConnect();
                return Ok(new { estado = "ok", base_de_datos = "conectada", timestamp = DateTime.Now });
            }
            catch
            {
                return StatusCode(503, new { estado = "error", mensaje = "No se pudo conectar a la base de datos." });
            }
        }
    }
}
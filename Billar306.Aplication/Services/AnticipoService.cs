using Billar306.API.Configuration;
using Billar306.API.Repositories;
using Billar306.Data.Models.Empleado;

namespace Billar306.API.Services
{
    public class AnticipoService
    {
        private readonly IAnticipoRepository _anticipoRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IConfiguracionRepository _configRepo;
        private readonly EventoService _eventoService;

        public AnticipoService(
            IAnticipoRepository anticipoRepo,
            IUsuarioRepository usuarioRepo,
            IConfiguracionRepository configRepo,
            EventoService eventoService)
        {
            _anticipoRepo = anticipoRepo;
            _usuarioRepo = usuarioRepo;
            _configRepo = configRepo;
            _eventoService = eventoService;
        }

        public async Task<List<T>> ObtenerTodosAsync()
            => await _anticipoRepo.ObtenerTodosAsync();

        public async Task<List<Anticipo>> ObtenerPorEmpleadoAsync(int empleadoId)
            => await _anticipoRepo.ObtenerPorEmpleadoAsync(empleadoId);

        public async Task<(bool ok, string error, bool requiereJefe, decimal acumulado, decimal limite)>
            VerificarLimiteAsync(int empleadoId)
        {
            var empleado = await _usuarioRepo.ObtenerPorIdAsync(empleadoId);
            if (empleado == null) return (false, "Empleado no encontrado.", false, 0, 0);

            var porcentaje = await _configRepo.ObtenerDecimalAsync(ConfiguracionKeys.LimiteAnticipoPorc, 40);
            var limite = empleado.SueldoBase * (porcentaje / 100);
            var desde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var acumulado = await _anticipoRepo.ObtenerAcumuladoPeriodoAsync(empleadoId, desde);

            if (acumulado >= limite)
                return (true, string.Empty, true, acumulado, limite);

            return (true, string.Empty, false, acumulado, limite);
        }

        public async Task<(bool ok, string error)> RegistrarAnticipoAsync(
            int empleadoId, int turnoId, int usuarioAutorizanteId,
            decimal monto, string rolAutorizante, bool forzar)
        {
            if (rolAutorizante == "empleado")
                return (false, "Solo el encargado o el jefe pueden registrar anticipos.");

            var empleado = await _usuarioRepo.ObtenerPorIdAsync(empleadoId);
            if (empleado == null) return (false, "Empleado no encontrado.");

            var porcentaje = await _configRepo.ObtenerDecimalAsync(ConfiguracionKeys.LimiteAnticipoPorc, 40);
            var limite = empleado.SueldoBase * (porcentaje / 100);
            var desde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var acumulado = await _anticipoRepo.ObtenerAcumuladoPeriodoAsync(empleadoId, desde);

            bool superaLimite = (acumulado + monto) > limite;

            if (superaLimite && !forzar)
                return (false, $"El anticipo supera el límite del {porcentaje}% del sueldo. Acumulado: ${acumulado}. Límite: ${limite}. Requiere autorización del jefe.");

            if (superaLimite && forzar && rolAutorizante != "jefe")
                return (false, "Solo el jefe puede forzar un anticipo que supera el límite.");

            var anticipo = new Anticipo
            {
                EmpleadoId = empleadoId,
                TurnoId = turnoId,
                UsuarioAutorizanteId = usuarioAutorizanteId,
                Monto = monto,
                Fecha = DateTime.Now,
                ForzadoPorJefe = forzar && superaLimite
            };

            await _anticipoRepo.AgregarAsync(anticipo);
            await _anticipoRepo.GuardarCambiosAsync();

            // registrar evento cuando el jefe fuerza el límite
            if (forzar && superaLimite)
                await _eventoService.RegistrarAnticipoExcedidoAsync(turnoId, usuarioAutorizanteId, monto, limite);

            return (true, string.Empty);
        }
    }
}
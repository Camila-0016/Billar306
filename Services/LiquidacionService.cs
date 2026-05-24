using Billar306.API.Configuration;
using Billar306.API.Models;
using Billar306.API.Repositories;

namespace Billar306.API.Services
{
    public class LiquidacionService
    {
        private readonly IRegistroHoraRepository _horaRepo;
        private readonly IAnticipoRepository _anticipoRepo;
        private readonly IFiadoRepository _fiadoRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IConfiguracionRepository _configRepo;

        public LiquidacionService(
            IRegistroHoraRepository horaRepo,
            IAnticipoRepository anticipoRepo,
            IFiadoRepository fiadoRepo,
            IUsuarioRepository usuarioRepo,
            IConfiguracionRepository configRepo)
        {
            _horaRepo = horaRepo;
            _anticipoRepo = anticipoRepo;
            _fiadoRepo = fiadoRepo;
            _usuarioRepo = usuarioRepo;
            _configRepo = configRepo;
        }

        public async Task<(bool ok, string error)> RegistrarEntradaAsync(int usuarioId, int turnoId)
        {
            var registroAbierto = await _horaRepo.ObtenerRegistroAbiertoAsync(usuarioId, turnoId);
            if (registroAbierto != null)
                return (false, "Ya tenés una entrada registrada en este turno sin salida.");

            var registro = new RegistroHoraEmpleado
            {
                UsuarioId = usuarioId,
                TurnoId = turnoId,
                Entrada = DateTime.Now
            };

            await _horaRepo.AgregarAsync(registro);
            await _horaRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }

        public async Task<(bool ok, string error)> RegistrarSalidaAsync(int usuarioId, int turnoId)
        {
            var registro = await _horaRepo.ObtenerRegistroAbiertoAsync(usuarioId, turnoId);
            if (registro == null)
                return (false, "No tenés una entrada registrada en este turno.");

            registro.Salida = DateTime.Now;
            registro.HorasTrabajadas = (decimal)(registro.Salida.Value - registro.Entrada).TotalHours;

            await _horaRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }

        public async Task<object> CalcularLiquidacionAsync(int usuarioId, DateTime desde, DateTime hasta)
        {
            var usuario = await _usuarioRepo.ObtenerPorIdAsync(usuarioId);
            if (usuario == null) return new { error = "Usuario no encontrado." };

            // Tarifa según rol
            string claveTargifa = usuario.Rol == "encargado"
                ? ConfiguracionKeys.TarifaHoraEncargado
                : ConfiguracionKeys.TarifaHoraEmpleado;
            var tarifaPorHora = await _configRepo.ObtenerDecimalAsync(claveTargifa, 0);

            // Horas trabajadas en el período
            var registros = await _horaRepo.ObtenerPorUsuarioYPeriodoAsync(usuarioId, desde, hasta);
            var totalHoras = registros.Sum(r => r.HorasTrabajadas ?? 0);
            var sueldoBruto = totalHoras * tarifaPorHora;

            // Anticipos del período
            var anticipos = await _anticipoRepo.ObtenerPorEmpleadoAsync(usuarioId);
            var totalAnticipos = anticipos
                .Where(a => a.Fecha >= desde && a.Fecha <= hasta)
                .Sum(a => a.Monto);

            // Fiados pendientes del empleado como deuda
            var fiados = await _fiadoRepo.ObtenerPorClienteAsync(usuarioId);
            var fiadosPendientes = fiados
                .Where(f => f.Estado == "Pendiente" || f.Estado == "Vencido")
                .Sum(f => f.MontoTotal);

            var sueldoNeto = sueldoBruto - totalAnticipos - fiadosPendientes;

            return new
            {
                Usuario = usuario.NombreCompleto,
                Rol = usuario.Rol,
                Desde = desde,
                Hasta = hasta,
                TotalHoras = Math.Round(totalHoras, 2),
                TarifaPorHora = tarifaPorHora,
                SueldoBruto = Math.Round(sueldoBruto, 2),
                TotalAnticipos = totalAnticipos,
                FiadosPendientes = fiadosPendientes,
                SueldoNeto = Math.Round(sueldoNeto, 2),
                DetalleHoras = registros.Select(r => new
                {
                    r.Entrada,
                    r.Salida,
                    Horas = Math.Round(r.HorasTrabajadas ?? 0, 2)
                })
            };
        }

        public async Task<List<object>> CalcularLiquidacionTodosAsync(DateTime desde, DateTime hasta)
        {
            var usuarios = await _usuarioRepo.ObtenerTodosAsync();
            var resultado = new List<object>();

            foreach (var usuario in usuarios.Where(u => u.Rol != "jefe"))
            {
                var liquidacion = await CalcularLiquidacionAsync(usuario.Id, desde, hasta);
                resultado.Add(liquidacion);
            }

            return resultado;
        }
    }
}
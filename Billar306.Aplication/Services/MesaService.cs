using Billar306.API.Configuration;
using Billar306.API.Repositories;
using Billar306.Data.Models.Venta.Mesa;

namespace Billar306.API.Services
{
    public class MesaService
    {
        private readonly IMesaRepository _mesaRepo;
        private readonly ISesionMesaRepository _sesionRepo;
        private readonly IConfiguracionRepository _configRepo;

        public MesaService(IMesaRepository mesaRepo, ISesionMesaRepository sesionRepo, IConfiguracionRepository configRepo)
        {
            _mesaRepo = mesaRepo;
            _sesionRepo = sesionRepo;
            _configRepo = configRepo;
        }

        public async Task<List<Mesa>> ObtenerTodasAsync()
            => await _mesaRepo.ObtenerTodasAsync();

        public async Task<(bool ok, string error)> AbrirMesaAsync(
            int mesaId, int usuarioId, int turnoId, int? clienteFrecuenteId)
        {
            var mesa = await _mesaRepo.ObtenerPorIdAsync(mesaId);
            if (mesa == null) return (false, "Mesa no encontrada.");
            if (mesa.Estado == "Ocupada") return (false, "La mesa ya está ocupada.");

            var sesion = new SesionMesa
            {
                MesaId = mesaId,
                UsuarioId = usuarioId,
                TurnoId = turnoId,
                ClienteFrecuenteId = clienteFrecuenteId,
                Inicio = DateTime.Now,
                Estado = "Abierta"
            };

            mesa.Estado = "Ocupada";
            await _sesionRepo.AgregarAsync(sesion);
            await _sesionRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }

        private async Task<decimal> CalcularTotalAsync(SesionMesa sesion, DateTime fin)
        {
            var tarifaPorHora = await _configRepo.ObtenerDecimalAsync(ConfiguracionKeys.TarifaHoraMesa, 0);
            var recargoPorcentaje = await _configRepo.ObtenerDecimalAsync(ConfiguracionKeys.RecargoPorcentajeNocturno, 50);
            var horaInicioRecargo = await _configRepo.ObtenerEnteroAsync(ConfiguracionKeys.HoraInicioRecargo, 6);
            var horaFinRecargo = await _configRepo.ObtenerEnteroAsync(ConfiguracionKeys.HoraFinRecargo, 14);

            var tarifaNormal = tarifaPorHora / 60; // por minuto
            var tarifaConRecargo = tarifaNormal * (1 + recargoPorcentaje / 100);

            var totalTiempo = 0m;
            var cursor = sesion.Inicio;

            // Calcular minuto a minuto si hay recargo intermedio
            while (cursor < fin)
            {
                var siguiente = cursor.AddMinutes(1);
                if (siguiente > fin) siguiente = fin;

                var hora = cursor.Hour;
                bool aplicaRecargo = hora >= horaInicioRecargo && hora < horaFinRecargo;

                var minutos = (decimal)(siguiente - cursor).TotalMinutes;
                totalTiempo += minutos * (aplicaRecargo ? tarifaConRecargo : tarifaNormal);
                cursor = siguiente;
            }

            var totalConsumiciones = sesion.Consumiciones.Sum(c => c.PrecioUnitario * c.Cantidad);
            return Math.Round(totalTiempo + totalConsumiciones, 2);
        }

        public async Task<(bool ok, string error, decimal total)> CerrarMesaAsync(
            int mesaId, int usuarioId, decimal montoRecibido,
            int? clienteFrecuenteId, string? prenda,
            string? descripcionPrenda, DateTime? fechaVencimiento)
        {
            var mesa = await _mesaRepo.ObtenerPorIdAsync(mesaId);
            if (mesa == null) return (false, "Mesa no encontrada.", 0);

            var sesion = await _sesionRepo.ObtenerSesionAbiertaPorMesaAsync(mesaId);
            if (sesion == null) return (false, "No hay sesión abierta en esta mesa.", 0);

            var fin = DateTime.Now;
            var total = await CalcularTotalAsync(sesion, fin);

            sesion.Fin = fin;
            sesion.TotalCobrado = total;
            sesion.MontoRecibido = montoRecibido;
            sesion.Vuelto = montoRecibido - total;
            mesa.Estado = "Libre";

            if (montoRecibido >= total)
            {
                sesion.Estado = "Cobrada";
            }
            else
            {
                if (clienteFrecuenteId == null || string.IsNullOrWhiteSpace(prenda) || fechaVencimiento == null)
                    return (false, "Pago parcial requiere cliente, prenda y fecha de vencimiento para registrar el fiado.", total);

                sesion.Estado = "Fiada";
            }

            await _sesionRepo.GuardarCambiosAsync();
            return (true, string.Empty, total);
        }

        public async Task<decimal> ObtenerTotalActualAsync(int mesaId)
        {
            var sesion = await _sesionRepo.ObtenerSesionAbiertaPorMesaAsync(mesaId);
            if (sesion == null) return 0;
            return await CalcularTotalAsync(sesion, DateTime.Now);
        }
    }
}
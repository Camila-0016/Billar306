// CuentaService.cs
using Billar306.Aplicacion.DTOs.Cuentas;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Control;

namespace Billar306.Aplicacion.Services
{
    public class CuentaService
    {
        private readonly ISesionMesaRepository _sesionMesaRepository;
        private readonly ICuentaBaseRepository _cuentaRepository;
        private readonly IMesaRepository _mesaRepository;
        private readonly IConfiguracionSistemaRepository _configRepository;

        public CuentaService(
            ISesionMesaRepository sesionMesaRepository,
            ICuentaBaseRepository cuentaRepository,
            IMesaRepository mesaRepository,
            IConfiguracionSistemaRepository configRepository)
        {
            _sesionMesaRepository = sesionMesaRepository;
            _cuentaRepository = cuentaRepository;
            _mesaRepository = mesaRepository;
            _configRepository = configRepository;
        }

        public async Task<CuentaDetalleDto?> ObtenerDetalleAsync(int id)
        {
            // Primero intenta como sesión de mesa
            var sesionMesa = await _sesionMesaRepository.ObtenerPorIdAsync(id);
            if (sesionMesa is not null)
            {
                var mesa = await _mesaRepository.ObtenerPorIdAsync(sesionMesa.MesaId);

                decimal montoMesaActual;
                if (sesionMesa.FechaFin is not null)
                {
                    montoMesaActual = sesionMesa.MontoSesionMesa;
                }
                else
                {
                    var tarifa = await _configRepository.ObtenerPorClaveAsync(TipoParametro.TarifaHoraMesa);
                    var horas = (decimal)(DateTime.UtcNow - sesionMesa.FechaInicio).TotalHours;
                    montoMesaActual = tarifa is null ? 0 : Math.Round(horas * tarifa.Valor, 2);
                }

                var confiteriaTotal = sesionMesa.Total - sesionMesa.MontoSesionMesa;
                var totalActual = montoMesaActual + confiteriaTotal;

                return new CuentaDetalleDto(
                    sesionMesa.Id, sesionMesa.MesaId, mesa?.Numero, sesionMesa.ClienteId, sesionMesa.TurnoId,
                    sesionMesa.EmpleadoAperturaId, sesionMesa.EmpleadoCierreId,
                    sesionMesa.FechaInicio, sesionMesa.FechaFin,
                    montoMesaActual, totalActual, sesionMesa.VentaConfiteriaId
                );
            }

            // Si no es sesión de mesa, es una venta directa
            var cuenta = await _cuentaRepository.ObtenerPorIdAsync(id);
            if (cuenta is null) return null;

            return new CuentaDetalleDto(
                cuenta.Id, null, null, cuenta.ClienteId, cuenta.TurnoId,
                cuenta.EmpleadoAperturaId, cuenta.EmpleadoCierreId,
                cuenta.FechaInicio, null,
                null, cuenta.Total, cuenta.VentaConfiteriaId
            );
        }
    }
}
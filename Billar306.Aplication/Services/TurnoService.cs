using Billar306.API.Configuration;
using Billar306.API.Repositories;
using Billar306.Data.Models.Control;
using Billar306.Data.Models.Operatividad;

namespace Billar306.API.Services
{
    public class TurnoService
    {
        private readonly ITurnoRepository _turnoRepo;
        private readonly ISesionMesaRepository _sesionRepo;
        private readonly IConfiguracionRepository _configRepo;
        private readonly IItemConfiteriaRepository _itemRepo;
        private readonly IIngresoStockRepository _ingresoRepo;
        private readonly IVentaDirectaRepository _ventaDirectaRepo;

        public TurnoService(
            ITurnoRepository turnoRepo,
            ISesionMesaRepository sesionRepo,
            IConfiguracionRepository configRepo,
            IItemConfiteriaRepository itemRepo,
            IIngresoStockRepository ingresoRepo,
            IVentaDirectaRepository ventaDirectaRepo)
        {
            _turnoRepo = turnoRepo;
            _sesionRepo = sesionRepo;
            _configRepo = configRepo;
            _itemRepo = itemRepo;
            _ingresoRepo = ingresoRepo;
            _ventaDirectaRepo = ventaDirectaRepo;
        }

        public async Task<(bool ok, string error, Turno? turno)> AbrirTurnoAsync(
            int usuarioId,
            List<(int itemId, int stockContado)> conteoStock)
        {
            var turnoExistente = await _turnoRepo.ObtenerTurnoAbiertoDeUsuarioAsync(usuarioId);
            if (turnoExistente != null)
                return (false, "Ya tenés un turno abierto.", null);

            var turno = new Turno
            {
                UsuarioId = usuarioId,
                FechaApertura = DateTime.Now,
                MontoAperturaCaja = 0,
                Estado = "Abierto"
            };

            await _turnoRepo.AgregarAsync(turno);
            await _turnoRepo.GuardarCambiosAsync();

            // Registrar stock de apertura por ítem
            foreach (var (itemId, stockContado) in conteoStock)
            {
                var item = await _itemRepo.ObtenerPorIdAsync(itemId);
                if (item == null) continue;

                item.StockApertura = stockContado;
                item.StockActual = stockContado;
            }

            await _itemRepo.GuardarCambiosAsync();
            return (true, string.Empty, turno);
        }

        public async Task<(bool ok, string error)> RegistrarIngresoStockAsync(
            int turnoId, int usuarioId, int itemId, int cantidad, string? nota)
        {
            var turno = await _turnoRepo.ObtenerPorIdAsync(turnoId);
            if (turno == null) return (false, "Turno no encontrado.");
            if (turno.Estado == "Cerrado") return (false, "El turno ya está cerrado.");

            var item = await _itemRepo.ObtenerPorIdAsync(itemId);
            if (item == null) return (false, "Ítem no encontrado.");

            // Sumar al stock actual
            item.StockActual += cantidad;

            var ingreso = new IngresoStock
            {
                TurnoId = turnoId,
                ItemConfiteriaId = itemId,
                Cantidad = cantidad,
                UsuarioId = usuarioId,
                Timestamp = DateTime.Now,
                Nota = nota
            };

            await _ingresoRepo.AgregarAsync(ingreso);
            await _ingresoRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }

        public async Task<(bool ok, string error)> CerrarTurnoAsync(
    int turnoId, decimal efectivoConfiteria, decimal efectivoMaquinas, string? nota)
        {
            var turno = await _turnoRepo.ObtenerPorIdAsync(turnoId);
            if (turno == null) return (false, "Turno no encontrado.");
            if (turno.Estado == "Cerrado") return (false, "El turno ya está cerrado.");

            var sesiones = await _sesionRepo.ObtenerPorTurnoAsync(turnoId);
            var hayMesasAbiertas = sesiones.Any(s => s.Estado == "Abierta");
            if (hayMesasAbiertas)
                return (false, "Hay mesas abiertas. Cerrá todas las mesas antes de cerrar el turno.");

            var totalCobrado = sesiones
                .Where(s => s.Estado == "Cobrada")
                .Sum(s => s.TotalCobrado ?? 0);

            var ventasDirectas = await _ventaDirectaRepo.ObtenerPorTurnoAsync(turnoId);
            var totalVentasDirectas = ventasDirectas.Sum(v => v.Total);
            var montoEsperado = totalCobrado + totalVentasDirectas;

            var montoCierreFisico = efectivoConfiteria + efectivoMaquinas;
            var diferencia = efectivoConfiteria - montoEsperado; 

            var montoMaxBaja = await _configRepo.ObtenerDecimalAsync(ConfiguracionKeys.MontoMaxGravedadBaja, 2000);
            string gravedad;
            if (Math.Abs(diferencia) <= montoMaxBaja)
                gravedad = "baja";
            else if (Math.Abs(diferencia) <= montoMaxBaja * 3)
                gravedad = "media";
            else
                gravedad = "alta";

            turno.FechaCierre = DateTime.Now;
            turno.EfectivoConfiteria = efectivoConfiteria;
            turno.EfectivoMaquinas = efectivoMaquinas;
            turno.MontoCierreFisico = montoCierreFisico;
            turno.MontoEsperado = montoEsperado; 
            turno.GravedadDiferencia = gravedad;
            turno.NotaCierre = nota;
            turno.Estado = "Cerrado";

            await _turnoRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }

        public async Task<List<IngresoStock>> ObtenerIngresosDelTurnoAsync(int turnoId)
            => await _ingresoRepo.ObtenerPorTurnoAsync(turnoId);

        public async Task<Turno?> ObtenerTurnoAbiertoAsync(int usuarioId)
            => await _turnoRepo.ObtenerTurnoAbiertoDeUsuarioAsync(usuarioId);
    }
}
using Billar306.API.Configuration;
using Billar306.API.Models;
using Billar306.API.Repositories;

namespace Billar306.API.Services
{
    public class EventoService
    {
        private readonly IEventoTurnoRepository _eventoRepo;
        private readonly IConfiguracionRepository _configRepo;

        public EventoService(IEventoTurnoRepository eventoRepo, IConfiguracionRepository configRepo)
        {
            _eventoRepo = eventoRepo;
            _configRepo = configRepo;
        }

        public async Task RegistrarAsync(int turnoId, int usuarioId, string tipo, string descripcion, string gravedad)
        {
            var evento = new EventoTurno
            {
                TurnoId = turnoId,
                UsuarioId = usuarioId,
                TipoEvento = tipo,
                Descripcion = descripcion,
                Gravedad = gravedad,
                Timestamp = DateTime.Now,
                Revisado = false
            };

            await _eventoRepo.AgregarAsync(evento);
            await _eventoRepo.GuardarCambiosAsync();
        }

        public async Task RegistrarDiferenciaCajaAsync(int turnoId, int usuarioId, decimal diferencia)
        {
            var montoMaxBaja = await _configRepo.ObtenerDecimalAsync(ConfiguracionKeys.MontoMaxGravedadBaja, 2000);
            string gravedad;
            string descripcion;

            if (Math.Abs(diferencia) <= montoMaxBaja)
            {
                gravedad = "baja";
                descripcion = $"Diferencia de caja de ${Math.Abs(diferencia):F2}. Dentro del rango de redondeo.";
            }
            else if (Math.Abs(diferencia) <= montoMaxBaja * 3)
            {
                gravedad = "media";
                descripcion = $"Diferencia de caja de ${Math.Abs(diferencia):F2}.";
            }
            else
            {
                gravedad = "alta";
                descripcion = $"Diferencia de caja de ${Math.Abs(diferencia):F2}. Supera el umbral máximo.";
            }

            await RegistrarAsync(turnoId, usuarioId, "diferencia_caja", descripcion, gravedad);
        }

        public async Task RegistrarDiscrepanciaAperturaAsync(int turnoId, int usuarioId, string detalle)
        {
            await RegistrarAsync(
                turnoId, usuarioId,
                "discrepancia_apertura",
                $"Discrepancia al abrir turno: {detalle}",
                "media"
            );
        }

        public async Task RegistrarAnticipoExcedidoAsync(int turnoId, int usuarioId, decimal monto, decimal limite)
        {
            await RegistrarAsync(
                turnoId, usuarioId,
                "anticipo_excedido",
                $"Anticipo de ${monto:F2} forzado por jefe. Límite era ${limite:F2}.",
                "alta"
            );
        }

        public async Task<List<EventoTurno>> ObtenerTodosAsync()
            => await _eventoRepo.ObtenerTodosAsync();

        public async Task<List<EventoTurno>> ObtenerPorUsuarioAsync(int usuarioId)
            => await _eventoRepo.ObtenerPorUsuarioAsync(usuarioId);

        public async Task<List<EventoTurno>> ObtenerAlertasAltaAsync()
            => await _eventoRepo.ObtenerPorGravedadAsync("alta");

        public async Task<(bool ok, string error)> MarcarRevisadoAsync(
            int eventoId, int usuarioRevisorId, string rol, string nota)
        {
            var evento = await _eventoRepo.ObtenerPorIdAsync(eventoId);
            if (evento == null) return (false, "Evento no encontrado.");
            if (evento.Revisado) return (false, "Este evento ya fue revisado.");

            if (evento.Gravedad == "alta" && rol != "jefe")
                return (false, "Solo el jefe puede marcar como revisado un evento de gravedad alta.");

            evento.Revisado = true;
            evento.NotaRevision = nota;
            evento.UsuarioRevisionId = usuarioRevisorId;

            await _eventoRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }
    }
}
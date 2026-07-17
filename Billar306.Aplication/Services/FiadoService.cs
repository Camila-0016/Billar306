
using Billar306.API.Models;
using Billar306.API.Repositories;

namespace Billar306.API.Services
{
    public class FiadoService
    {
        private readonly IFiadoRepository _fiadoRepo;
        private readonly ISesionMesaRepository _sesionRepo;
        private readonly IMesaRepository _mesaRepo;
        private readonly IAbonoFiadoRepository _abonoRepo;

        public FiadoService(
            IFiadoRepository fiadoRepo,
            ISesionMesaRepository sesionRepo,
            IMesaRepository mesaRepo,
            IAbonoFiadoRepository abonoRepo)
        {
            _fiadoRepo = fiadoRepo;
            _sesionRepo = sesionRepo;
            _mesaRepo = mesaRepo;
            _abonoRepo = abonoRepo;
        }

        public async Task<List<Fiado>> ObtenerTodosAsync()
            => await _fiadoRepo.ObtenerTodosAsync();

        public async Task<(bool ok, string error)> RegistrarFiadoAsync(
            int mesaId, int clienteFrecuenteId, int usuarioId,
            string prenda, string? descripcionPrenda, DateTime fechaVencimiento)
        {
            if (string.IsNullOrWhiteSpace(prenda))
                return (false, "La prenda es obligatoria para registrar un fiado.");

            if (fechaVencimiento <= DateTime.Now)
                return (false, "El plazo de vencimiento debe ser una fecha futura.");

            var prendasValidas = new[] { "telefono", "reloj", "taco", "otro" };
            if (!prendasValidas.Contains(prenda.ToLower()))
                return (false, "Prenda inválida. Usá: telefono, reloj, taco u otro.");

            var sesion = await _sesionRepo.ObtenerSesionAbiertaPorMesaAsync(mesaId);
            if (sesion == null)
                return (false, "No hay sesión abierta en esa mesa.");

            var totalConsumiciones = sesion.Consumiciones.Sum(c => c.PrecioUnitario * c.Cantidad);
            var minutos = (decimal)(DateTime.Now - sesion.Inicio).TotalMinutes;
            decimal tarifaPorMinuto = 10m;
            var total = (minutos * tarifaPorMinuto) + totalConsumiciones;

            var fiado = new Fiado
            {
                SesionMesaId = sesion.Id,
                ClienteFrecuenteId = clienteFrecuenteId,
                UsuarioRegistroId = usuarioId,
                MontoTotal = total,
                MontoAbonado = 0,
                Prenda = prenda.ToLower(),
                DescripcionPrenda = descripcionPrenda,
                FechaRegistro = DateTime.Now,
                FechaVencimiento = fechaVencimiento,
                Estado = "Pendiente"
            };

            sesion.Estado = "Fiada";
            sesion.Fin = DateTime.Now;
            sesion.TotalCobrado = total;

            var mesa = await _mesaRepo.ObtenerPorIdAsync(mesaId);
            if (mesa != null) mesa.Estado = "Libre";

            await _fiadoRepo.AgregarAsync(fiado);
            await _fiadoRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }

        public async Task<(bool ok, string error)> RegistrarAbonoAsync(
            int fiadoId, int usuarioId, decimal monto, string? nota)
        {
            var fiado = await _fiadoRepo.ObtenerPorIdAsync(fiadoId);
            if (fiado == null) return (false, "Fiado no encontrado.");
            if (fiado.Estado == "Cobrado") return (false, "Este fiado ya fue cobrado.");

            if (monto <= 0)
                return (false, "El monto del abono debe ser mayor a cero.");

            var pendiente = fiado.MontoTotal - fiado.MontoAbonado;
            if (monto > pendiente)
                return (false, $"El abono supera el monto pendiente de ${pendiente:F2}.");

            fiado.MontoAbonado += monto;

            // Si el abono cubre el total, cerrar el fiado
            if (fiado.MontoAbonado >= fiado.MontoTotal)
            {
                fiado.Estado = "Cobrado";
                fiado.FechaPago = DateTime.Now;
                fiado.UsuarioCierreId = usuarioId;
            }

            var abono = new AbonoFiado
            {
                FiadoId = fiadoId,
                UsuarioId = usuarioId,
                Monto = monto,
                Fecha = DateTime.Now,
                Nota = nota
            };

            await _abonoRepo.AgregarAsync(abono);
            await _fiadoRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }

        public async Task<(bool ok, string error)> CobrarFiadoAsync(int fiadoId, int usuarioId)
        {
            var fiado = await _fiadoRepo.ObtenerPorIdAsync(fiadoId);
            if (fiado == null) return (false, "Fiado no encontrado.");
            if (fiado.Estado == "Cobrado") return (false, "Este fiado ya fue cobrado.");

            fiado.MontoAbonado = fiado.MontoTotal;
            fiado.Estado = "Cobrado";
            fiado.FechaPago = DateTime.Now;
            fiado.UsuarioCierreId = usuarioId;

            var abono = new AbonoFiado
            {
                FiadoId = fiadoId,
                UsuarioId = usuarioId,
                Monto = fiado.MontoTotal - (fiado.MontoAbonado - fiado.MontoTotal),
                Fecha = DateTime.Now,
                Nota = "Pago total"
            };

            await _abonoRepo.AgregarAsync(abono);
            await _fiadoRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }

        public async Task ActualizarFiadosVencidosAsync()
        {
            var fiados = await _fiadoRepo.ObtenerTodosAsync();
            var vencidos = fiados.Where(f =>
                f.Estado == "Pendiente" &&
                f.FechaVencimiento < DateTime.Now).ToList();

            foreach (var fiado in vencidos)
                fiado.Estado = "Vencido";

            if (vencidos.Any())
                await _fiadoRepo.GuardarCambiosAsync();
        }

        public async Task<(bool ok, string error)> RegistrarFiadoDirectoAsync(
    int clienteFrecuenteId, int usuarioId,
    decimal monto, string prenda,
    string? descripcionPrenda, DateTime fechaVencimiento)
        {
            if (string.IsNullOrWhiteSpace(prenda))
                return (false, "La prenda es obligatoria.");

            if (fechaVencimiento <= DateTime.Now)
                return (false, "El plazo de vencimiento debe ser una fecha futura.");

            var prendasValidas = new[] { "telefono", "reloj", "taco", "otro" };
            if (!prendasValidas.Contains(prenda.ToLower()))
                return (false, "Prenda inválida. Usá: telefono, reloj, taco u otro.");

            if (monto <= 0)
                return (false, "El monto debe ser mayor a cero.");

            var fiado = new Fiado
            {
                SesionMesaId = null, // sin mesa
                ClienteFrecuenteId = clienteFrecuenteId,
                UsuarioRegistroId = usuarioId,
                MontoTotal = monto,
                MontoAbonado = 0,
                Prenda = prenda.ToLower(),
                DescripcionPrenda = descripcionPrenda,
                FechaRegistro = DateTime.Now,
                FechaVencimiento = fechaVencimiento,
                Estado = "Pendiente"
            };

            await _fiadoRepo.AgregarAsync(fiado);
            await _fiadoRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }

        public async Task<bool> TieneClienteFiadoVencidoAsync(int clienteId)
            => await _fiadoRepo.TieneClienteFiadoVencidoAsync(clienteId);
    }
}
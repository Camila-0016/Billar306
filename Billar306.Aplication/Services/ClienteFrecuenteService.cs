using Billar306.API.Models;
using Billar306.API.Repositories;

namespace Billar306.API.Services
{
    public class ClienteFrecuenteService
    {
        private readonly IClienteFrecuenteRepository _clienteRepo;
        private readonly IFiadoRepository _fiadoRepo;

        public ClienteFrecuenteService(IClienteFrecuenteRepository clienteRepo, IFiadoRepository fiadoRepo)
        {
            _clienteRepo = clienteRepo;
            _fiadoRepo = fiadoRepo;
        }

        public async Task<List<ClienteFrecuente>> ObtenerTodosAsync()
            => await _clienteRepo.ObtenerTodosActivosAsync();

        public async Task<(bool ok, string error, bool esduplicado, ClienteFrecuente? existente)>
            CrearClienteAsync(string nombreCompleto, string? telefono)
        {
            // verificar duplicado
            var existente = await _clienteRepo.BuscarPorNombreAsync(nombreCompleto);
            if (existente != null)
                return (false, "Ya existe un cliente con ese nombre.", true, existente);

            var cliente = new ClienteFrecuente
            {
                NombreCompleto = nombreCompleto,
                Telefono = telefono,
                Activo = true,
                FechaRegistro = DateTime.Now
            };

            await _clienteRepo.AgregarAsync(cliente);
            await _clienteRepo.GuardarCambiosAsync();
            return (true, string.Empty, false, cliente);
        }

        public async Task<(bool ok, string error)> DesactivarAsync(int id)
        {
            var cliente = await _clienteRepo.ObtenerPorIdAsync(id);
            if (cliente == null) return (false, "Cliente no encontrado.");

            cliente.Activo = false;
            await _clienteRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }

        public async Task<object?> ObtenerResumenAsync(int clienteId)
        {
            var cliente = await _clienteRepo.ObtenerPorIdAsync(clienteId);
            if (cliente == null) return null;

            var fiados = await _fiadoRepo.ObtenerPorClienteAsync(clienteId);
            var tieneVencido = await _fiadoRepo.TieneClienteFiadoVencidoAsync(clienteId);

            return new
            {
                cliente.Id,
                cliente.NombreCompleto,
                cliente.Telefono,
                TieneFiadoVencido = tieneVencido,
                FiadosPendientes = fiados.Count(f => f.Estado == "Pendiente"),
                TotalFiadoPendiente = fiados.Where(f => f.Estado == "Pendiente").Sum(f => f.MontoTotal)
            };
        }
    }
}
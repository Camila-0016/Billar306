using Billar306.Aplicacion.DTOs.Clientes;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Clientes;

namespace Billar306.Aplicacion.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<List<ClienteDto>> ObtenerTodosAsync()
        {
            var clientes = await _clienteRepository.ObtenerTodosAsync();
            return clientes.Select(MapearADto).ToList();
        }

        public async Task<ClienteDto?> ObtenerPorIdAsync(int id)
        {
            var cliente = await _clienteRepository.ObtenerPorIdAsync(id);
            return cliente is null ? null : MapearADto(cliente);
        }

        public async Task<List<ClienteDto>> BuscarPorNombreAsync(string nombre)
        {
            var clientes = await _clienteRepository.BuscarPorNombreAsync(nombre);
            return clientes.Select(MapearADto).ToList();
        }

        public async Task<ClienteDto> AgregarAsync(CrearClienteDto dto)
        {
            var nuevoCliente = new Cliente
            {
                NombreCompleto = dto.NombreCompleto
            };

            await _clienteRepository.AgregarAsync(nuevoCliente);
            return MapearADto(nuevoCliente);
        }

        public async Task<bool> ActualizarAsync(int id, ActualizarClienteDto dto)
        {
            var clienteExistente = await _clienteRepository.ObtenerPorIdAsync(id);
            if (clienteExistente is null) return false;

            clienteExistente.NombreCompleto = dto.NombreCompleto;
            clienteExistente.Activo = dto.Activo;

            await _clienteRepository.ActualizarAsync(clienteExistente);
            return true;
        }

        private static ClienteDto MapearADto(Cliente c)
            => new(c.Id, c.NombreCompleto, c.Activo, c.FechaInicio);
    }
}
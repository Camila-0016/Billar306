using Billar306.Aplicacion.DTOs.Mesas;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Venta.Mesas;

namespace Billar306.Aplicacion.Services
{
    public class MesaService
    {
        private readonly IMesaRepository _mesaRepository;

        public MesaService(IMesaRepository mesaRepository)
        {
            _mesaRepository = mesaRepository;
        }

        public async Task<List<MesaDto>> ObtenerTodasAsync()
        {
            var mesas = await _mesaRepository.ObtenerTodosAsync();
            return mesas.Select(MapearADto).ToList();
        }

        public async Task<MesaDto?> ObtenerPorIdAsync(int id)
        {
            var mesa = await _mesaRepository.ObtenerPorIdAsync(id);
            return mesa is null ? null : MapearADto(mesa);
        }

        public async Task<(bool Exito, string? Error, MesaDto? Mesa)> CrearAsync(CrearMesaDto dto)
        {
            if (await _mesaRepository.ObtenerPorNumeroAsync(dto.Numero) is not null)
                return (false, "Ya existe una mesa con ese número.", null);

            var mesa = new Mesa { Numero = dto.Numero, Ocupada = false };
            await _mesaRepository.AgregarAsync(mesa);

            return (true, null, MapearADto(mesa));
        }

        private static MesaDto MapearADto(Mesa m) => new(m.Id, m.Numero, m.Ocupada);
    }
}
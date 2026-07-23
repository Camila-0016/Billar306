using Billar306.Aplicacion.DTOs.Confiteria;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Control;

namespace Billar306.Aplicacion.Services
{
    public class CatalogoService
    {
        private readonly ICatalogoRepository _catalogoRepository;

        public CatalogoService(ICatalogoRepository catalogoRepository)
        {
            _catalogoRepository = catalogoRepository;
        }

        public async Task<List<CatalogoDto>> ObtenerTodosAsync()
        {
            var catalogos = await _catalogoRepository.ObtenerTodosAsync();
            return catalogos.Select(c => new CatalogoDto(c.Id, c.Categoria)).ToList();
        }

        public async Task<CatalogoDto?> ObtenerPorIdAsync(int id)
        {
            var catalogo = await _catalogoRepository.ObtenerPorIdAsync(id);
            return catalogo is null ? null : new CatalogoDto(catalogo.Id, catalogo.Categoria);
        }

        public async Task<CatalogoDto> CrearAsync(CrearCatalogoDto dto)
        {
            var catalogo = new Catalogo { Categoria = dto.Categoria };
            await _catalogoRepository.AgregarAsync(catalogo);
            return new CatalogoDto(catalogo.Id, catalogo.Categoria);
        }
    }
}
using Billar306.Aplicacion.DTOs.Confiteria;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Control;

namespace Billar306.Aplicacion.Services
{
    public class ProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ICatalogoRepository _catalogoRepository;

        public ProductoService(IProductoRepository productoRepository, ICatalogoRepository catalogoRepository)
        {
            _productoRepository = productoRepository;
            _catalogoRepository = catalogoRepository;
        }

        public async Task<List<ProductoDto>> ObtenerTodosAsync()
        {
            var productos = await _productoRepository.ObtenerTodosAsync();
            return productos.Select(MapearADto).ToList();
        }

        public async Task<List<ProductoDto>> ObtenerPorCatalogoAsync(int catalogoId)
        {
            var productos = await _productoRepository.ObtenerPorCatalogoAsync(catalogoId);
            return productos.Select(MapearADto).ToList();
        }

        public async Task<ProductoDto?> ObtenerPorIdAsync(int id)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(id);
            return producto is null ? null : MapearADto(producto);
        }

        public async Task<(bool Exito, string? Error, ProductoDto? Producto)> CrearAsync(CrearProductoDto dto)
        {
            if (await _catalogoRepository.ObtenerPorIdAsync(dto.CatalogoId) is null)
                return (false, "El catálogo indicado no existe.", null);

            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Descripcion = dto.Descripcion ?? string.Empty,
                CatalogoId = dto.CatalogoId,
                Stock = 0,
                StockMinimo = 0
            };
            await _productoRepository.AgregarAsync(producto);
            return (true, null, MapearADto(producto));
        }

        public async Task<bool> ActualizarAsync(int id, ActualizarProductoDto dto)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(id);
            if (producto is null) return false;

            producto.Nombre = dto.Nombre;
            producto.Precio = dto.Precio;
            producto.Descripcion = dto.Descripcion ?? "";
            producto.Activo = dto.Activo;
            await _productoRepository.ActualizarAsync(producto);
            return true;
        }

        private static ProductoDto MapearADto(Producto p)
            => new(p.Id, p.Nombre, p.Precio, p.Descripcion, p.CatalogoId, p.Activo);
    }
}
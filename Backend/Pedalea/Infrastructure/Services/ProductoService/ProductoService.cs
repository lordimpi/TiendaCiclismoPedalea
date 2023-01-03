using DataAccess.Models.Entities;
using DataAccess.Repositories.ProductoRepository;
using System.Data.SqlClient;
using System.Data;
using DataAccess.Data;

namespace Infrastructure.Services.ProductoService
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }
        public async Task<Producto> AddProductoAsync(Producto producto)
        {
            return await _productoRepository.AddProductoAsync(producto);
        }

        public async Task<bool> DeleteProductoAsync(int id)
        {
            return await _productoRepository.DeleteProductoAsync(id);
        }

        public async Task<Producto> GetProductoAsync(int id)
        {
            return await _productoRepository.GetProductoAsync(id);
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            return await _productoRepository.GetProductosAsync();
        }

        public async Task<Producto> UpdateProductoAsync(int id,Producto producto)
        {
            return await _productoRepository.UpdateProductoAsync(id, producto);
        }
    }
}

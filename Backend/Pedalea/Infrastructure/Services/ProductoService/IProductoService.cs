using DataAccess.Models.Entities;

namespace Infrastructure.Services.ProductoService
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> GetProductosAsync();
        Task<Producto> GetProductoAsync(int id);
        Task<Producto> AddProductoAsync(Producto producto);
        Task<Producto> UpdateProductoAsync(int id, Producto producto);
        Task<bool> DeleteProductoAsync(int id);
    }
}

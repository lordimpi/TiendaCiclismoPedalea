using DataAccess.Models.Entities;

namespace DataAccess.Repositories.ProductoRepository
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetProductosAsync();
        Task<Producto> GetProductoAsync(int id);
        Task<Producto> AddProductoAsync(Producto producto);
        Task<Producto> UpdateProductoAsync(int id,Producto producto);
        Task<bool> DeleteProductoAsync(int id);
    }
}

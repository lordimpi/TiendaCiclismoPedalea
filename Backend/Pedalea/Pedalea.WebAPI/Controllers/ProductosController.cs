using DataAccess.Models.Entities;
using Infrastructure.Services.ProductoService;
using Microsoft.AspNetCore.Mvc;

namespace Pedalea.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet("GetProducts/")]
        public async Task<IActionResult> GetProducts()
        {
            IEnumerable<Producto> products = await _productoService.GetProductosAsync();
            return Ok(products);
        }

        [HttpGet("GetProduct/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            Producto product = await _productoService.GetProductoAsync(id);
            return Ok(product);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(Producto producto)
        {
            Producto product = await _productoService.AddProductoAsync(producto);
            return Ok(product);
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Producto producto)
        {
            Producto product = await _productoService.UpdateProductoAsync(id, producto);
            return Ok(product);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            bool product = await _productoService.DeleteProductoAsync(id);
            return Ok(product);
        }
    }
}

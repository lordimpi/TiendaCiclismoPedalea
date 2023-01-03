using DataAccess.Models.Entities;
using Infrastructure.Services.PedidoService;
using Microsoft.AspNetCore.Mvc;

namespace Pedalea.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet("GetPedidos")]
        public async Task<IActionResult> GetPedidos()
        {
            IEnumerable<Pedido> pedidos = await _pedidoService.GetPedidosAsync();
            return Ok(pedidos);
        }

        [HttpGet("GetPedido/{id}")]
        public async Task<IActionResult> GetPedido(int id)
        {
            Pedido pedido = await _pedidoService.GetPedidoByIdAsync(id);
            return Ok(pedido);
        }

        [HttpPost("AddPedido")]
        public async Task<IActionResult> AddPedido(Pedido pedido)
        {
            Pedido pedidoN = await _pedidoService.CreatePedidoAsync(pedido);
            return Ok(pedidoN);
        }

        [HttpPut("UpdatePedido/{id}")]
        public async Task<IActionResult> UpdatePedido(int id,Pedido pedido)
        {
            Pedido pedidoN = await _pedidoService.UpdatePedidoAsync(id,pedido);
            return Ok(pedidoN);
        }

        [HttpDelete("DeletePedido/{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            bool result = await _pedidoService.DeletePedidoAsync(id);
            return Ok(result);
        }
    }
}

using DataAccess.Models.Entities;
using DataAccess.Repositories.PedidoRepository;

namespace Infrastructure.Services.PedidoService
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Pedido> CreatePedidoAsync(Pedido newPedido)
        {
            return await _pedidoRepository.AddPedidoAsync(newPedido);
        }

        public async Task<bool> DeletePedidoAsync(int id)
        {
            return await _pedidoRepository.DeletePedidoAsync(id);
        }

        public async Task<Pedido> GetPedidoByIdAsync(int id)
        {
            return await _pedidoRepository.GetPedidoAsync(id);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosAsync()
        {
            return await _pedidoRepository.GetPedidosAsync();
        }

        public async Task<Pedido> UpdatePedidoAsync(int id, Pedido pedido)
        {
            return await _pedidoRepository.UpdatePedidoAsync(id, pedido);
        }
    }
}

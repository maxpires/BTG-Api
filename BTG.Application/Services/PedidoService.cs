using BTG.Domain.Contracts.Repositories;
using BTG.Domain.Contracts.Services;
using BTG.Domain.InputOutput;

namespace BTG.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository ?? throw new ArgumentNullException(nameof(pedidoRepository));
        }

        public async Task<IEnumerable<PedidoOutput>> GetListaPedidosPorCliente(int codigoCliente)
        {
            return await _pedidoRepository.GetListaPedidosPorCliente(codigoCliente);
        }

        public async Task<QtdPedidoClienteOutput?> GetQuantidadePedidosPorCliente(int codigoCliente)
        {
            return await _pedidoRepository.GetQuantidadePedidosPorCliente(codigoCliente);
        }

        public async Task<ValorTotalPedidoOutput?> GetValorTotalDoPedido(int codigoPedido)
        {
            return await _pedidoRepository.GetValorTotalDoPedido(codigoPedido);
        }

        public async Task<bool> Inserir(PedidoInput pedido)
        {
            throw new NotImplementedException();
        }
    }
}

using BTG.Domain.InputOutput;

namespace BTG.Domain.Contracts.Repositories
{
    public interface IPedidoRepository
    {
        Task<ValorTotalPedidoOutput?> GetValorTotalDoPedido(int codigoPedido);
        Task<QtdPedidoClienteOutput?> GetQuantidadePedidosPorCliente(int codigoCliente);
        Task<IEnumerable<PedidoOutput>> GetListaPedidosPorCliente(int codigoCliente);
    }
}

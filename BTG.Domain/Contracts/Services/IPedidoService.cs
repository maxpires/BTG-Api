using BTG.Domain.InputOutput;
using BTG.Domain.Models;

namespace BTG.Domain.Contracts.Services
{
    public interface IPedidoService
    {
        Task<ValorTotalPedidoOutput?> GetValorTotalDoPedido(int codigoPedido);
        Task<QtdPedidoClienteOutput?> GetQuantidadePedidosPorCliente(int codigoCliente);
        Task<IEnumerable<PedidoOutput>> GetListaPedidosPorCliente(int codigoCliente);
        Task<bool> Inserir(PedidoInput pedido);
    }
}

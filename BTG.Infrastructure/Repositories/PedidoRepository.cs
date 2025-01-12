using BTG.Domain.Contracts.Repositories;
using BTG.Domain.InputOutput;
using Microsoft.EntityFrameworkCore;

namespace BTG.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DefaultContext _db;
        public PedidoRepository(DefaultContext db)
        {
            _db = db ?? throw new ArgumentException(nameof(db));
        }

        public async Task<IEnumerable<PedidoOutput>> GetListaPedidosPorCliente(int codigoCliente)
        {

            var pedidos = await _db.Pedido
                    .Include(p => p.DetalhesPedidos)
                    .Where(p => p.CodigoCliente == codigoCliente)
                    .Select(p => new PedidoOutput
                    {
                        CodigoPedido = p.CodigoPedido,
                        CodigoCliente = p.Cliente.CodigoCliente,
                        Itens = (List<Item>)p.DetalhesPedidos.Select(dp => new Item
                        {
                            Produto = dp.Produto.Produto,
                            Quantidade = dp.Produto.Quantidade,
                            Preco = dp.Produto.Preco
                        })
                    })
                    .ToListAsync();

            return pedidos;
        }

        public async Task<QtdPedidoClienteOutput?> GetQuantidadePedidosPorCliente(int codigoCliente)
        {
            var quantidadePedidos = await _db.Pedido
                .CountAsync(p => p.CodigoCliente == codigoCliente);

            if (quantidadePedidos == 0)
                return null;

            return new QtdPedidoClienteOutput
            {
                CodigoCliente = codigoCliente,
                QuantidadePedidos = quantidadePedidos
            };
        }

        public async Task<ValorTotalPedidoOutput?> GetValorTotalDoPedido(int codigoPedido)
        {
            var pedido = await _db.DetalhesPedido
                .Include(x => x.Produto)
                .Where(dp => dp.CodigoPedido == codigoPedido).ToListAsync();

            if (pedido.Count() == 0)
                return null;

            var total = pedido.Sum(x => x.Produto.Quantidade * x.Produto.Preco);

            return new ValorTotalPedidoOutput
            {
                CodigoPedido = codigoPedido,
                ValorTotal = total
            };         
        }
    }
}

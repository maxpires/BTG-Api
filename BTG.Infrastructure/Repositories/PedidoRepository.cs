using BTG.Domain.Contracts.Repositories;
using BTG.Domain.Entities;
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
                            Produto = dp.Produto,
                            Quantidade = dp.Quantidade,
                            Preco = dp.Preco
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
                .Where(dp => dp.CodigoPedido == codigoPedido).ToListAsync();

            if (pedido.Count() == 0)
                return null;

            var total = pedido.Sum(x => x.Quantidade * x.Preco);

            return new ValorTotalPedidoOutput
            {
                CodigoPedido = codigoPedido,
                ValorTotal = total
            };         
        }

        public async Task<bool> Inserir(PedidoInput pedido)
        {

            var exitePedido = await _db.Pedido.Where(x => x.CodigoPedido == pedido.codigoPedido).FirstOrDefaultAsync();
            if (exitePedido != null)
            {
                throw new Exception($"Pedido { pedido.codigoPedido } já processado.");
            }


            _db.Database.BeginTransaction();

            var cliente = await _db.Cliente.Where(x => x.CodigoCliente == pedido.codigoCliente).FirstOrDefaultAsync();
            if (cliente == null)
            {
                _db.Add(new ClienteEntity
                {
                    CodigoCliente = pedido.codigoCliente
                });
            }

            var pedidoEntity = new PedidoEntity
            {
                CodigoPedido = pedido.codigoPedido,
                CodigoCliente = pedido.codigoCliente,
                DetalhesPedidos = pedido.itens.Select(x => new DetalhesPedidoEntity
                {
                    CodigoPedido = pedido.codigoPedido,
                    Produto = x.produto,
                    Quantidade = x.quantidade,
                    Preco = x.preco
                }).ToList()
            };

            _db.Add(pedidoEntity);

            var ret = await _db.SaveChangesAsync() > 0 ? true : false;
            
            _db.Database.CommitTransaction();

            return ret;
        }
    }
}

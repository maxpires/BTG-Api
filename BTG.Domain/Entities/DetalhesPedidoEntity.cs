using System.ComponentModel.DataAnnotations;

namespace BTG.Domain.Entities
{
    public class DetalhesPedidoEntity
    {
        public int CodigoPedido { get; set; }
        public int CodigoProduto { get; set; }


        public PedidoEntity Pedido { get; set; }
        public ProdutoEntity Produto { get; set; }
    }
}

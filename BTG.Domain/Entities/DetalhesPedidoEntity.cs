namespace BTG.Domain.Entities
{
    public class DetalhesPedidoEntity
    {
        public int CodigoDetalhesPedido { get; set; }
        public int CodigoPedido { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }

        public PedidoEntity Pedido { get; set; }
    }
}

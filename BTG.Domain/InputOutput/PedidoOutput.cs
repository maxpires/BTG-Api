namespace BTG.Domain.InputOutput
{
    public class PedidoOutput
    {
        public int CodigoPedido { get; set; }
        public int CodigoCliente { get; set; }
        public List<Item>? Itens { get; set; }
    }

    public class Item
    {
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}

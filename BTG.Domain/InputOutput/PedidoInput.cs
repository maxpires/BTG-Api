namespace BTG.Domain.InputOutput
{
    public class PedidoInput
    {
        public int codigoPedido { get; set; }
        public int codigoCliente { get; set; }
        public List<ItemPedido> itens { get; set; }
    }

    public class ItemPedido
    {
        public string produto { get; set; }
        public int quantidade { get; set; }
        public decimal preco { get; set; }
    }
}

namespace BTG.Domain.Entities
{
    public class PedidoEntity
    {
        public int CodigoPedido { get; set; }
        public int CodigoCliente { get; set; }

        public ClienteEntity Cliente { get; set; }
        public ICollection<DetalhesPedidoEntity> DetalhesPedidos { get; set; }
    }
}

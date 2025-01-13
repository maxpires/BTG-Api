namespace BTG.Domain.Entities
{
    public class ClienteEntity
    {
        public int CodigoCliente { get; set; }
        public ICollection<PedidoEntity>? Pedidos { get; set; }
    }
}

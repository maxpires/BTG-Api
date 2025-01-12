namespace BTG.Domain.Entities
{
    public class ProdutoEntity
    {
        public int CodigoProduto { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public ICollection<DetalhesPedidoEntity> DetalhesPedidos { get; set; }
    }
}

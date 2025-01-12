using BTG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BTG.Infrastructure.Map
{
    public class ProdutoMap : EntityTypeConfiguration<ProdutoEntity>
    {
        public override void Map(EntityTypeBuilder<ProdutoEntity> builder)
        {
            builder.ToTable("Produto");
            builder.HasKey(x => x.CodigoProduto);
            builder.Property(x => x.CodigoProduto).ValueGeneratedOnAdd();
            builder.Property<decimal>("Preco").HasColumnType("decimal(10,2)");
            builder.Property(x => x.Produto).IsRequired();
            builder.Property(x => x.Preco).IsRequired();
        }
    }
}

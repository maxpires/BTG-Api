using BTG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BTG.Infrastructure.Map
{
    public class DetalhesPedidoMap : EntityTypeConfiguration<DetalhesPedidoEntity>
    {
        public override void Map(EntityTypeBuilder<DetalhesPedidoEntity> builder)
        {
            builder.ToTable("DetalhesPedido");
            builder.HasKey(x => x.CodigoDetalhesPedido);
            builder.Property(x => x.CodigoDetalhesPedido).ValueGeneratedOnAdd();
            builder.Property(x => x.Preco).HasColumnType("decimal(10,2)");
        }
    }
}

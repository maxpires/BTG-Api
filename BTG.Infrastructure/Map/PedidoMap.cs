using BTG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BTG.Infrastructure.Map
{
    public class PedidoMap : EntityTypeConfiguration<PedidoEntity>
    {
        public override void Map(EntityTypeBuilder<PedidoEntity> builder)
        {
            builder.ToTable("Pedido");
            builder.HasKey(x => x.CodigoPedido);
            builder.Property(x => x.CodigoPedido).ValueGeneratedNever();

            builder
                .HasMany(x => x.DetalhesPedidos)
                .WithOne(x => x.Pedido)
                .HasForeignKey(x => x.CodigoPedido)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

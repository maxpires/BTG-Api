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
            builder.HasKey(dp => new { dp.CodigoPedido, dp.CodigoProduto });

            builder.HasOne(dp => dp.Pedido)
                  .WithMany(p => p.DetalhesPedidos)
                  .HasForeignKey(dp => dp.CodigoPedido)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dp => dp.Produto)
                  .WithMany(pr => pr.DetalhesPedidos)
                  .HasForeignKey(dp => dp.CodigoProduto)
                  .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

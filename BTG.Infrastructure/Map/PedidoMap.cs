using Azure;
using BTG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace BTG.Infrastructure.Map
{
    public class PedidoMap : EntityTypeConfiguration<PedidoEntity>
    {
        public override void Map(EntityTypeBuilder<PedidoEntity> builder)
        {
            builder.ToTable("Pedido");
            builder.HasKey(x => x.CodigoPedido);
            builder.Property(x => x.CodigoPedido).ValueGeneratedOnAdd();            

            builder.HasOne(p => p.Cliente)
                  .WithMany(c => c.Pedidos)
                  .HasForeignKey(p => p.CodigoCliente);

            
        }
    }
}

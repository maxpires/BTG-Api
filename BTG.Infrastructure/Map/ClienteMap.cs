using BTG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BTG.Infrastructure.Map
{
    public class ClienteMap : EntityTypeConfiguration<ClienteEntity>
    {
        public override void Map(EntityTypeBuilder<ClienteEntity> builder)
        {
            builder.ToTable("Cliente");
            builder.HasKey(x => x.CodigoCliente);
            builder.Property(x => x.CodigoCliente).ValueGeneratedNever();

            builder
                .HasMany(x => x.Pedidos)
                .WithOne(x => x.Cliente)
                .HasForeignKey(x => x.CodigoCliente);
        }
    }
}

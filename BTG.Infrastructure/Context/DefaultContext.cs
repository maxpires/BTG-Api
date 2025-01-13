using BTG.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BTG.Infrastructure
{
    public class DefaultContext : DbContext
    {
        public DbSet<ClienteEntity> Cliente => Set<ClienteEntity>();
        public DbSet<PedidoEntity> Pedido => Set<PedidoEntity>();
        public DbSet<DetalhesPedidoEntity> DetalhesPedido => Set<DetalhesPedidoEntity>();

        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());

                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            modelBuilder.RegisterModelsMapping();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}

using BTG.Domain.Entities;
using BTG.Infrastructure.Map;
using Microsoft.EntityFrameworkCore;

namespace BTG.Infrastructure
{
    public static class ModelBuiderExtensions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder, EntityTypeConfiguration<TEntity> configuration) where TEntity : class
        {
            configuration.Map(modelBuilder.Entity<TEntity>());
        }

        public static void RegisterModelsMapping(this ModelBuilder modelBuilder)
        {
            new ClienteMap().Map(modelBuilder.Entity<ClienteEntity>());
            new PedidoMap().Map(modelBuilder.Entity<PedidoEntity>());
            new DetalhesPedidoMap().Map(modelBuilder.Entity<DetalhesPedidoEntity>());
        }
    }
}

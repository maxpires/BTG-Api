using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BTG.Infrastructure
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}

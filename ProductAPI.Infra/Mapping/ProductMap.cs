using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Domain.Entities;

namespace ProductAPI.Infra.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasKey(e => e.ProductId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CostPrice).IsRequired();
            entity.Property(e => e.SellPrice).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValue(DateTime.UtcNow);
        }
    }
}

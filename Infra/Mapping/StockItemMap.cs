using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    class StockItemMap : IEntityTypeConfiguration<StockItem>
    {
        public void Configure(EntityTypeBuilder<StockItem> builder)

        {
            builder.ToTable("StockItem");

            builder.HasKey(e => e.Id);
            builder.HasAlternateKey(e => new { e.ProductId, e.StoreId });

            builder.HasOne(e => e.Product);

            builder.HasOne(e => e.Store);

            builder.Property(e => e.Quantity)
                 .HasColumnName("Quantity")
                 .IsRequired()
                 .IsConcurrencyToken();
        }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    class StoreMap : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)

        {
            builder.ToTable("Store");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                 .HasColumnName("Id")
                 .IsRequired();

            builder.Property(e => e.Name)
                 .HasColumnName("Name")
                 .IsRequired();

            builder.Property(e => e.Address)
                 .HasColumnName("Address")
                 .IsRequired();
        }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mapping
{
    class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)

        {
            builder.ToTable("Product");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                 .HasColumnName("Id")
                 .IsRequired();

            builder.Property(e => e.Name)
                 .HasColumnName("Name")
                 .IsRequired();

            builder.Property(e => e.Price)
                 .HasColumnName("Price")
                 .IsRequired();
        }
    }
}

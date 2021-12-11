using Domain.Entities;
using Infra.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StockItem> StockItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new StockItemMap());
            modelBuilder.ApplyConfiguration(new StoreMap());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
            }
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}

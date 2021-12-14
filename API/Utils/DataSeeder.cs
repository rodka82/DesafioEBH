using Domain.Entities;
using Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utils
{
    public static class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Reset();

            var product1 = new Product()
            {
                Id = 1,
                Name = "Produto 1",
                Price = 10.5
            };

            var product2 = new Product()
            {
                Id = 2,
                Name = "Produto 2",
                Price = 20.5
            };

            var product3 = new Product()
            {
                Id = 3,
                Name = "Produto 3",
                Price = 30.5
            };

            context.Products.AddRange(product1, product2, product3);

            var store1 = new Store()
            {
                Id = 1,
                Name = "Loja 1",
                Address = "Endereço Loja 1"
            };

            var store2 = new Store()
            {
                Id = 2,
                Name = "Loja 2",
                Address = "Endereço Loja 2"
            };

            var store3 = new Store()
            {
                Id = 3,
                Name = "Loja 3",
                Address = "Endereço Loja 3"
            };

            context.Stores.AddRange(store1, store2, store3);

            var stockItem1 = new StockItem()
            {
                Id = 1,
                Product = product1,
                Store = store1,
                ProductId = product1.Id,
                StoreId = store1.Id,
                Quantity = 10
            };

            var stockItem2 = new StockItem()
            {
                Id = 2,
                Product = product2,
                Store = store2,
                ProductId = product2.Id,
                StoreId = store2.Id,
                Quantity = 20
            };

            var stockItem3 = new StockItem()
            {
                Id = 3,
                Product = product3,
                Store = store3,
                ProductId = product3.Id,
                StoreId = store3.Id,
                Quantity = 30
            };

            context.StockItems.AddRange(stockItem1, stockItem2, stockItem3);

            context.SaveChanges();
        }
    }
}

using Domain.Entities;
using Infra.Context;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Repositories
{
    public class ProductRepositoryTest
    {
        public class CrudValidation : ProductRepositoryTest
        {
            [Fact]
            public void ShouldAddProduct()
            {
                var product = new Product();

                var context = new Mock<DbContext>();
                var dbSetMock = new Mock<DbSet<Product>>();
                context.Setup(x => x.Set<Product>()).Returns(dbSetMock.Object);

                dbSetMock.Setup(x => x.Add(It.IsAny<Product>()));

                var repository = new Repository<Product>(context.Object);
                repository.Add(product);

                context.Verify(x => x.Set<Product>());
                dbSetMock.Verify(x => x.Add(It.Is<Product>(y => y == product)));
            }

            [Fact]
            public void ShouldUpdateProduct()
            {
                var context = new Mock<DbContext>();
                var dbSetMock = new Mock<DbSet<Product>>();
                context.Setup(x => x.Set<Product>()).Returns(dbSetMock.Object);

                var product = new Product();

                var sourceList = new List<Product>();
                sourceList.Add(product);

                dbSetMock.Setup(x => x.Update(It.IsAny<Product>()));

                var repository = new Repository<Product>(context.Object);
                repository.Update(product);

                context.Verify(x => x.Set<Product>());
                dbSetMock.Verify(x => x.Update(It.Is<Product>(y => y == product)));
            }

            [Fact]
            public void ShouldRemoveProduct()
            {
                var product = new Product();

                var context = new Mock<DbContext>();
                var dbSetMock = new Mock<DbSet<Product>>();
                context.Setup(x => x.Set<Product>()).Returns(dbSetMock.Object);
                dbSetMock.Setup(x => x.Remove(It.IsAny<Product>()));

                var repository = new Repository<Product>(context.Object);
                repository.Delete(product);

                context.Verify(x => x.Set<Product>());
                dbSetMock.Verify(x => x.Remove(It.Is<Product>(y => y == product)));
            }

            [Fact]
            public void ShouldGetProduct()
            {
                var product = new Product();

                var context = new Mock<DbContext>();
                var dbSetMock = new Mock<DbSet<Product>>();

                context.Setup(x => x.Set<Product>()).Returns(dbSetMock.Object);
                dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(product);

                var repository = new Repository<Product>(context.Object);
                repository.GetById(1);

                context.Verify(x => x.Set<Product>());
                dbSetMock.Verify(x => x.Find(It.IsAny<int>()));
            }
        }
    }
}

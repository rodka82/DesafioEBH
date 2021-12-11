using Domain.Entities;
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
    public class StockItemRepositoryTest
    {
        public class CrudValidation : StockItemRepositoryTest
        {
            [Fact]
            public void ShouldAddStockItem()
            {
                var stockItem = new StockItem();

                var context = new Mock<DbContext>();
                var dbSetMock = new Mock<DbSet<StockItem>>();
                context.Setup(x => x.Set<StockItem>()).Returns(dbSetMock.Object);

                dbSetMock.Setup(x => x.Add(It.IsAny<StockItem>()));

                var repository = new Repository<StockItem>(context.Object);
                repository.Add(stockItem);

                context.Verify(x => x.Set<StockItem>());
                dbSetMock.Verify(x => x.Add(It.Is<StockItem>(y => y == stockItem)));
            }

            [Fact]
            public void ShouldUpdateStockItem()
            {
                var context = new Mock<DbContext>();
                var dbSetMock = new Mock<DbSet<StockItem>>();
                context.Setup(x => x.Set<StockItem>()).Returns(dbSetMock.Object);

                var stockItem = new StockItem();

                var sourceList = new List<StockItem>();
                sourceList.Add(stockItem);

                dbSetMock.Setup(x => x.Update(It.IsAny<StockItem>()));

                var repository = new Repository<StockItem>(context.Object);
                repository.Update(stockItem);

                context.Verify(x => x.Set<StockItem>());
                dbSetMock.Verify(x => x.Update(It.Is<StockItem>(y => y == stockItem)));
            }

            [Fact]
            public void ShouldRemoveStockItem()
            {
                var stockItem = new StockItem();

                var context = new Mock<DbContext>();
                var dbSetMock = new Mock<DbSet<StockItem>>();
                context.Setup(x => x.Set<StockItem>()).Returns(dbSetMock.Object);
                dbSetMock.Setup(x => x.Remove(It.IsAny<StockItem>()));

                var repository = new Repository<StockItem>(context.Object);
                repository.Delete(stockItem);

                context.Verify(x => x.Set<StockItem>());
                dbSetMock.Verify(x => x.Remove(It.Is<StockItem>(y => y == stockItem)));
            }

            [Fact]
            public void ShouldGetStockItem()
            {
                var stockItem = new StockItem();

                var context = new Mock<DbContext>();
                var dbSetMock = new Mock<DbSet<StockItem>>();

                context.Setup(x => x.Set<StockItem>()).Returns(dbSetMock.Object);
                dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(stockItem);

                var repository = new Repository<StockItem>(context.Object);
                repository.GetById(1);

                context.Verify(x => x.Set<StockItem>());
                dbSetMock.Verify(x => x.Find(It.IsAny<int>()));
            }
        }
    }
}

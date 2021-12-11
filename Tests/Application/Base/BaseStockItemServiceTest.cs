using Domain.Entities;
using Infra.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Services
{
    public class BaseStockItemServiceTest : BaseServiceTest<StockItem>
    {
        protected override void ConfigureUpdate(Mock<IRepository<StockItem>> repository)
        {
            repository.Setup(r => r.Update(It.IsAny<StockItem>()))
                            .Returns((StockItem StockItem) =>
                            {
                                var existentStockItem = _databaseEntities.First(s => s.Id == 1);
                                existentStockItem.Quantity = StockItem.Quantity;
                                return true;
                            })
                            .Callback<StockItem>(StockItem => StockItem.Id = 1);
        }
    }
}
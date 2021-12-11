using Domain.Entities;
using Infra.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Services
{
    public class BaseStockItemServiceTest
    {
        protected IRepository<StockItem> _repository;
        protected List<StockItem> _databaseStockItems;

        protected void ResetRepository()
        {
            _databaseStockItems = new List<StockItem>();
            _repository = GenerateRepositoryMock();
        }

        protected IRepository<StockItem> GenerateRepositoryMock()
        {
            var repository = new Mock<IRepository<StockItem>>();
            ConfigureRepositoryMock(repository);
            return repository.Object;
        }

        private void ConfigureRepositoryMock(Mock<IRepository<StockItem>> repository)
        {
            ConfigureGetById(repository);
            ConfigureAdd(repository);
            ConfigureUpdate(repository);
            ConfigureDelete(repository);
        }

        private void ConfigureAdd(Mock<IRepository<StockItem>> repository)
        {
            repository.Setup(r => r.Add(It.IsAny<StockItem>()))
                            .Returns((StockItem StockItem) =>
                            {
                                _databaseStockItems.Add(StockItem);
                                return true;
                            })
                            .Callback<StockItem>(StockItem => StockItem.Id = 1);
        }

        private void ConfigureDelete(Mock<IRepository<StockItem>> repository)
        {
            repository.Setup(r => r.Delete(It.IsAny<StockItem>()))
                            .Returns((StockItem StockItem) =>
                            {
                                _databaseStockItems.Remove(StockItem);
                                return true;
                            });
        }

        private void ConfigureGetById(Mock<IRepository<StockItem>> repository)
        {
            repository.Setup(r => r.GetById(It.IsAny<int>()))
                            .Returns((int id) => _databaseStockItems.Where(x => x.Id == id).FirstOrDefault());
        }

        private void ConfigureUpdate(Mock<IRepository<StockItem>> repository)
        {
            repository.Setup(r => r.Update(It.IsAny<StockItem>()))
                            .Returns((StockItem StockItem) =>
                            {
                                var existentStockItem = _databaseStockItems.First(s => s.Id == 1);
                                existentStockItem.Quantity = StockItem.Quantity;
                                return true;
                            })
                            .Callback<StockItem>(StockItem => StockItem.Id = 1);
        }
    }
}
using Domain.Entities;
using Infra.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Services
{
    public class BaseProductServiceTest
    {
        protected IRepository<Product> _repository;
        protected List<Product> _databaseProducts;

        protected void ResetRepository()
        {
            _databaseProducts = new List<Product>();
            _repository = GenerateRepositoryMock();
        }

        protected IRepository<Product> GenerateRepositoryMock()
        {
            var repository = new Mock<IRepository<Product>>();
            ConfigureRepositoryMock(repository);
            return repository.Object;
        }

        private void ConfigureRepositoryMock(Mock<IRepository<Product>> repository)
        {
            ConfigureGetById(repository);
            ConfigureAdd(repository);
            ConfigureUpdate(repository);
            ConfigureDelete(repository);
        }

        private void ConfigureAdd(Mock<IRepository<Product>> repository)
        {
            repository.Setup(r => r.Add(It.IsAny<Product>()))
                            .Returns((Product Product) =>
                            {
                                _databaseProducts.Add(Product);
                                return true;
                            })
                            .Callback<Product>(Product => Product.Id = 1);
        }

        private void ConfigureDelete(Mock<IRepository<Product>> repository)
        {
            repository.Setup(r => r.Delete(It.IsAny<Product>()))
                            .Returns((Product Product) =>
                            {
                                _databaseProducts.Remove(Product);
                                return true;
                            });
        }

        private void ConfigureGetById(Mock<IRepository<Product>> repository)
        {
            repository.Setup(r => r.GetById(It.IsAny<int>()))
                            .Returns((int id) => _databaseProducts.Where(x => x.Id == id).FirstOrDefault());
        }

        private void ConfigureUpdate(Mock<IRepository<Product>> repository)
        {
            repository.Setup(r => r.Update(It.IsAny<Product>()))
                            .Returns((Product Product) =>
                            {
                                var existentProduct = _databaseProducts.First(s => s.Id == 1);
                                existentProduct.Name = Product.Name;
                                existentProduct.Price = Product.Price;
                                return true;
                            })
                            .Callback<Product>(Product => Product.Id = 1);
        }
    }
}
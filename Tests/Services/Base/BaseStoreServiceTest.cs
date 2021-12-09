using Domain.Entities;
using Infra.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Services
{
    public class BaseStoreServiceTest
    {
        protected IStoreRepository _repository;
        protected List<Store> _databaseStores;

        protected void ResetRepository()
        {
            _databaseStores = new List<Store>();
            _repository = GenerateRepositoryMock();
        }

        protected IStoreRepository GenerateRepositoryMock()
        {
            var repository = new Mock<IStoreRepository>();
            ConfigureRepositoryMock(repository);
            return repository.Object;
        }

        private void ConfigureRepositoryMock(Mock<IStoreRepository> repository)
        {
            ConfigureGetById(repository);
            ConfigureAdd(repository);
            ConfigureUpdate(repository);
            ConfigureDelete(repository);
        }

        private void ConfigureAdd(Mock<IStoreRepository> repository)
        {
            repository.Setup(r => r.Add(It.IsAny<Store>()))
                            .Returns((Store store) =>
                            {
                                _databaseStores.Add(store);
                                return true;
                            })
                            .Callback<Store>(store => store.Id = 1);
        }

        private void ConfigureDelete(Mock<IStoreRepository> repository)
        {
            repository.Setup(r => r.Delete(It.IsAny<Store>()))
                            .Returns((Store store) =>
                            {
                                _databaseStores.Remove(store);
                                return true;
                            });
        }

        private void ConfigureGetById(Mock<IStoreRepository> repository)
        {
            repository.Setup(r => r.GetById(It.IsAny<int>()))
                            .Returns((int id) => _databaseStores.Where(x => x.Id == id).FirstOrDefault());
        }

        private void ConfigureUpdate(Mock<IStoreRepository> repository)
        {
            repository.Setup(r => r.Update(It.IsAny<Store>()))
                            .Returns((Store store) =>
                            {
                                var existentStore = _databaseStores.First(s => s.Id == 1);
                                existentStore.Name = store.Name;
                                existentStore.Address = store.Address;
                                return true;
                            })
                            .Callback<Store>(store => store.Id = 1);
        }
    }
}
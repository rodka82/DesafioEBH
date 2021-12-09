using Application.Services;
using Application.Validators;
using Domain.Entities;
using Infra.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Services
{
    public class StoreServiceTest
    {
        protected readonly IStoreValidator _validator;
        protected readonly IStoreService _service;
        protected IStoreRepository _repository;
        protected List<Store> _databaseStores;

        public StoreServiceTest()
        {
            ResetRepository();
            _validator = new StoreValidator();
            _service = new StoreService(_validator, _repository);
        }

        protected void ResetRepository()
        {
            _databaseStores = new List<Store>();
            _repository = GenerateRepositoryMock();
        }

        private IStoreRepository GenerateRepositoryMock()
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

        public class NameValidation : StoreServiceTest
        {
            [Fact]
            public void ShouldValidateNullStoreName()
            {
                var store = new Store();
                store.Name = null;
                var response = _service.Save(store);
                Assert.Contains(response.Messages, m => m.Contains("Nome da loja não pode ser nulo"));
            }

            [Fact]
            public void ShouldValidateEmptyStoreName()
            {
                var store = new Store();
                store.Name = string.Empty;
                store.Address = "Address";
                var response = _service.Save(store);
                Assert.Contains(response.Messages, m => m.Contains("Nome da loja não pode ser vazio"));
            }

            [Fact]
            public void ShouldValidateBlankStringStoreName()
            {
                var store = new Store();
                store.Name = " ";
                store.Address = "Address";
                var response = _service.Save(store);
                Assert.Contains(response.Messages, m => m.Contains("Nome da loja não pode ser vazio"));
            }

        }

        public class AddressValidation : StoreServiceTest
        {
            [Fact]
            public void ShouldValidateNullStoreAddress()
            {
                var store = new Store();
                store.Name = "Store Name";
                store.Address = null;
                var response = _service.Save(store);
                Assert.Contains(response.Messages, m => m.Contains("O Endereço não pode ser nulo"));
            }

            [Fact]
            public void ShouldValidateEmptyAddress()
            {
                var store = new Store();
                store.Name = "Store Name";
                store.Address = string.Empty;
                var response = _service.Save(store);
                Assert.Contains(response.Messages, m => m.Contains("O Endereço não pode ser vazio"));
            }

            [Fact]
            public void ShouldValidateBlankStringAddress()
            {
                var store = new Store();
                store.Name = "Store Name";
                store.Address = " ";
                var response = _service.Save(store);
                Assert.Contains(response.Messages, m => m.Contains("O Endereço não pode ser vazio"));
            }
        }

        public class CrudValidation : StoreServiceTest
        {
            [Fact]
            public void ShouldAddValidStore()
            {
                Store store = GenerateValidStore();

                _service.Save(store);

                var result = _repository.GetById(1);

                Assert.NotNull(result);
                Assert.IsType<Store>(result);
                Assert.Equal("Store Name", result.Name);

                ResetRepository();
            }


            [Fact]
            public void ShouldUpdateValidStore()
            {
                var store = GenerateValidStore();

                _service.Save(store);

                var storeToUpdate = GenerateValidStore();
                storeToUpdate.Id = 1;
                storeToUpdate.Name = "Updated Name";
                storeToUpdate.Address = "Updated Address";

                _service.Save(storeToUpdate);

                var result = _repository.GetById(1);

                Assert.NotNull(result);
                Assert.IsType<Store>(result);
                Assert.Equal("Updated Name", result.Name);
                Assert.Equal("Updated Address", result.Address);

                ResetRepository();
            }

            [Fact]
            public void ShouldDeleteStore()
            {
                var store = GenerateValidStore();

                _service.Save(store);

                _service.Delete(store);

                var result = _repository.GetById(1);

                Assert.Null(result);

                ResetRepository();
            }

            private static Store GenerateValidStore()
            {
                var store = new Store();
                store.Name = "Store Name";
                store.Address = "Store Address";
                return store;
            }

        }
    }
}

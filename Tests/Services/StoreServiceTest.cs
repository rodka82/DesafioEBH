using Application.Services;
using Application.Validators;
using Domain.Entities;
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

        public StoreServiceTest()
        {
            _validator = new StoreValidator();
            _service = new StoreService(_validator);
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
    }
}

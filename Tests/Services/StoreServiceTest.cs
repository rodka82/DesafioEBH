using Application.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Services
{
    public class StoreServiceTest
    {
        public class NameValidation
        {
            [Fact]
            public void ShouldValidateNullStoreName()
            {
                var store = new Store();
                store.Name = null;
                var service = new StoreService();

                Assert.Throws<ArgumentNullException>(() => service.Save(store));
            }

            [Fact]
            public void ShouldValidateEmptyStoreName()
            {
                var store = new Store();
                store.Name = string.Empty;
                store.Address = "Address";
                var service = new StoreService();

                Assert.Throws<ArgumentException>(() => service.Save(store));
            }

            [Fact]
            public void ShouldValidateBlankStringStoreName()
            {
                var store = new Store();
                store.Name = " ";
                store.Address = "Address";
                var service = new StoreService();

                Assert.Throws<ArgumentException>(() => service.Save(store));
            }

        }

        public class AddressValidation
        {
            [Fact]
            public void ShouldValidateNullStoreAddress()
            {
                var store = new Store();
                store.Name = "Store Name";
                store.Address = null;
                var service = new StoreService();

                Assert.Throws<ArgumentNullException>(() => service.Save(store));
            }

            [Fact]
            public void ShouldValidateEmptyAddress()
            {
                var store = new Store();
                store.Name = "Store Name";
                store.Address = string.Empty;
                var service = new StoreService();

                Assert.Throws<ArgumentException>(() => service.Save(store));
            }

            [Fact]
            public void ShouldValidateBlankStringAddress()
            {
                var store = new Store();
                store.Name = "Store Name";
                store.Address = " ";
                var service = new StoreService();

                Assert.Throws<ArgumentException>(() => service.Save(store));
            }
        }
    }
}

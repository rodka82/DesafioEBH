using Domain.Entities;
using Infra.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Services
{
    public class BaseStoreServiceTest : BaseServiceTest<Store>
    {
        protected override void ConfigureUpdate(Mock<IRepository<Store>> repository)
        {
            repository.Setup(r => r.Update(It.IsAny<Store>()))
                            .Returns((Store store) =>
                            {
                                var existentStore = _databaseEntities.First(s => s.Id == 1);
                                existentStore.Name = store.Name;
                                existentStore.Address = store.Address;
                                return true;
                            })
                            .Callback<Store>(store => store.Id = 1);
        }
    }
}
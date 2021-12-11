using Domain.Entities;
using Infra.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Services
{
    public class BaseProductServiceTest : BaseServiceTest<Product>
    {
        protected override void ConfigureUpdate(Mock<IRepository<Product>> repository)
        {
            repository.Setup(r => r.Update(It.IsAny<Product>()))
                            .Returns((Product product) =>
                            {
                                var existentProduct = _databaseEntities.First(s => s.Id == 1);
                                existentProduct.Name = product.Name;
                                existentProduct.Price = product.Price;
                                return true;
                            })
                            .Callback<Product>(product => product.Id = 1);
        }
    }
}
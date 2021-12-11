using Domain.Entities;
using Infra.Context;
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
    public class StoreRepositoryTest
    {
        public class CrudValidation : StoreRepositoryTest
        {
            [Fact]
            public void ShouldAddStore()
            {
                var store = new Store();

                var context = new Mock<DbContext>();
                var dbSetMock = new Mock<DbSet<Store>>();
                context.Setup(x => x.Set<Store>()).Returns(dbSetMock.Object);

                dbSetMock.Setup(x => x.Add(It.IsAny<Store>()));

                var repository = new Repository<Store>(context.Object);
                repository.Add(store);

                context.Verify(x => x.Set<Store>());
                dbSetMock.Verify(x => x.Add(It.Is<Store>(y => y == store)));
            }

            [Fact]
            public void ShouldUpdateStore()
            {
                var context = new Mock<DbContext>();
                var dbSetMock = new Mock<DbSet<Store>>();
                context.Setup(x => x.Set<Store>()).Returns(dbSetMock.Object);

                var store = new Store();

                var sourceList = new List<Store>();
                sourceList.Add(store);

                dbSetMock.Setup(x => x.Update(It.IsAny<Store>()));

                var repository = new Repository<Store>(context.Object);
                repository.Update(store);

                context.Verify(x => x.Set<Store>());
                dbSetMock.Verify(x => x.Update(It.Is<Store>(y => y == store)));
            }

            [Fact]
            public void ShouldRemoveStore()
            {
                var store = new Store();

                var context = new Mock<DbContext>();
                var dbSetMock = new Mock<DbSet<Store>>();
                context.Setup(x => x.Set<Store>()).Returns(dbSetMock.Object);
                dbSetMock.Setup(x => x.Remove(It.IsAny<Store>()));

                var repository = new Repository<Store>(context.Object);
                repository.Delete(store);

                context.Verify(x => x.Set<Store>());
                dbSetMock.Verify(x => x.Remove(It.Is<Store>(y => y == store)));
            }

            [Fact]
            public void ShouldGetStore()
            {
                var store = new Store();

                var context = new Mock<DbContext>();
                var dbSetMock = new Mock<DbSet<Store>>();

                context.Setup(x => x.Set<Store>()).Returns(dbSetMock.Object);
                dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(store);

                var repository = new Repository<Store>(context.Object);
                repository.GetById(1);

                context.Verify(x => x.Set<Store>());
                dbSetMock.Verify(x => x.Find(It.IsAny<int>()));
            }
        }
    }
}

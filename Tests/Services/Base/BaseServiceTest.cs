using Domain.Entities;
using Infra.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Services
{
    public abstract class BaseServiceTest<T> where T: BaseEntity
    {
        protected IRepository<T> _repository;
        protected List<T> _databaseEntities;

        protected void ResetRepository()
        {
            _databaseEntities = new List<T>();
            _repository = GenerateRepositoryMock();
        }

        protected IRepository<T> GenerateRepositoryMock()
        {
            var repository = new Mock<IRepository<T>>();
            ConfigureRepositoryMock(repository);
            return repository.Object;
        }

        private void ConfigureRepositoryMock(Mock<IRepository<T>> repository)
        {
            ConfigureGetById(repository);
            ConfigureAdd(repository);
            ConfigureUpdate(repository);
            ConfigureDelete(repository);
        }

        private void ConfigureAdd(Mock<IRepository<T>> repository)
        {
            repository.Setup(r => r.Add(It.IsAny<T>()))
                            .Returns((T entity) =>
                            {
                                _databaseEntities.Add(entity);
                                return true;
                            })
                            .Callback<T>(entity => entity.Id = 1);
        }

        private void ConfigureDelete(Mock<IRepository<T>> repository)
        {
            repository.Setup(r => r.Delete(It.IsAny<T>()))
                            .Returns((T entity) =>
                            {
                                _databaseEntities.Remove(entity);
                                return true;
                            });
        }

        private void ConfigureGetById(Mock<IRepository<T>> repository)
        {
            repository.Setup(r => r.GetById(It.IsAny<int>()))
                            .Returns((int id) => _databaseEntities.Where(x => x.Id == id).FirstOrDefault());
        }

        protected abstract void ConfigureUpdate(Mock<IRepository<T>> repository);
    }
}
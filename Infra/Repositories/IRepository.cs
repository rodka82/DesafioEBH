using Domain.Entities;

namespace Infra.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
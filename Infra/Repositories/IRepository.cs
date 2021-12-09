namespace Infra.Repositories
{
    public interface IRepository<T>
    {
        T GetById(int id);
        bool Add(T store);
        bool Update(T store);
        bool Delete(T store);
    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }
        public bool Add(T entity)
        {
            if (entity == null)
                return false;

            _entities.Add(entity);
            return true;
        }

        public bool Delete(T entity)
        {
            if (entity == null)
                return false;

            _entities.Remove(entity);
            return true;
        }

        public T GetById(int id)
        {
            return _entities.Find(id);
        }

        public bool Update(T entity)
        {
            if (entity == null)
                return false;

            _entities.Update(entity);
            return true;
        }
    }
}

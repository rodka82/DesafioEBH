using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repositories
{
    public interface IStoreRepository
    {
        Store GetById(int id);
        bool Add(Store store);
        bool Update(Store store);
        bool Delete(Store store);
    }
}

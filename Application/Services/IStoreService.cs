using Application.Utils;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Services
{
    public interface IStoreService
    {
        Store GetById(int id);
        IApplicationResponse Save(Store store);
        void Delete(Store store);
    }
}
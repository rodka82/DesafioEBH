using Application.Utils;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Services
{
    public interface IService<T> where T: BaseEntity
    {
        T GetById(int id);
        IApplicationResponse Save(T store);
        void Delete(T store);
    }
}
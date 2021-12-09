using Application.Utils;
using Domain.Entities;

namespace Application.Services
{
    public interface IProductService
    {
        Product GetById(int id);
        IApplicationResponse Save(Product store);
        void Delete(Product store);
    }
}
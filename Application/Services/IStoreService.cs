using Application.Utils;
using Domain.Entities;

namespace Application.Services
{
    public interface IStoreService
    {
        IApplicationResponse Save(Store store);
    }
}
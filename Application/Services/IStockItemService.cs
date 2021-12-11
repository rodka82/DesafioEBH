using Application.Utils;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Services
{
    public interface IStockItemService : IService<StockItem>
    {
        IApplicationResponse UpdateStock(StockItem stockitem);
    }
}
using Application.Utils;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface IStockItemService
    {
        StockItem GetById(int id);
        IApplicationResponse Save(StockItem store);
        void Delete(StockItem store);
    }
}

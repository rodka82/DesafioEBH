using Application.Utils;
using Application.Validators;
using Domain.Entities;
using Domain.Extensions;
using Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class StockItemService : BaseService<StockItem>, IStockItemService
    {
        public StockItemService(IValidator<StockItem> validator, IRepository<StockItem> repository)
            :base(validator, repository)
        {
        }

        public IApplicationResponse UpdateStock(StockItem stockItem)
        {
            var existentStockItem = _repository.GetById(stockItem.Id);
            UpdateQuantity(stockItem, existentStockItem);
            _repository.Update(existentStockItem);
            //TODO: acertar isso
            return new ApplicationResponse();
        }

        private static void UpdateQuantity(StockItem stockItem, StockItem existentStockItem)
        {
            if (stockItem.OperationType == OperationType.Increment)
                existentStockItem.Quantity += stockItem.Quantity;
            else if (stockItem.OperationType == OperationType.Decrement)
                Decrement(stockItem, existentStockItem);

        }

        private static void Decrement(StockItem stockItem, StockItem existentStockItem)
        {
            existentStockItem.Quantity -= stockItem.Quantity;
            existentStockItem.Quantity = existentStockItem.Quantity < 0 ? 0 : existentStockItem.Quantity;
        }
    }
}

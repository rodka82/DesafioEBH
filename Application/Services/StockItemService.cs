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
        protected readonly new IStockItemValidator _validator;
        public StockItemService(IStockItemValidator validator, IRepository<StockItem> repository)
            :base(validator, repository)
        {
            _validator = validator;
        }

        public IApplicationResponse UpdateStock(StockOperation operation)
        {
            var existentStockItem = _repository.GetById(operation.StockItemId);
            
            _validator.Validate(operation, existentStockItem);

            if (!_validator.IsValid)
                return ReturnValidationErrorResponse();

            UpdateQuantity(operation, existentStockItem);
            
            _repository.Update(existentStockItem);
            
            return ReturnSuccessResponse();
        }

        private void UpdateQuantity(StockOperation operation, StockItem existentStockItem)
        {
            if (operation.OperationType == OperationType.Increment)
            {
                existentStockItem.Quantity += operation.Quantity;
            }            
            else if (operation.OperationType == OperationType.Decrement)
            {
                existentStockItem.Quantity -= operation.Quantity;
            }
        }
    }
}

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
            
            if(existentStockItem == null)
            {
                _validator.ErrorMessages.Add("Item de estoque não encontrado");
                return ReturnValidationErrorResponse(existentStockItem);
            }

            _validator.Validate(operation, existentStockItem);

            if (!_validator.IsValid)
                return ReturnValidationErrorResponse(existentStockItem);

            UpdateQuantity(operation, existentStockItem);
            
            _repository.Update(existentStockItem);
            
            return ReturnSuccessResponse("Estoque alterado com sucesso", existentStockItem);
        }

        private void UpdateQuantity(StockOperation operation, StockItem existentStockItem)
        {
            if (operation.OperationType == StockOperationType.Increment)
            {
                existentStockItem.Quantity += operation.Quantity;
            }            
            else if (operation.OperationType == StockOperationType.Decrement)
            {
                existentStockItem.Quantity -= operation.Quantity;
            }
        }
    }
}

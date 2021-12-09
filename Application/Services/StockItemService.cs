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
    public class StockItemService : IStockItemService
    {
        private readonly IStockItemValidator _validator;
        private readonly IStockItemRepository _repository;

        public StockItemService(IStockItemValidator validator, IStockItemRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public StockItem GetById(int id)
        {
            var store = _repository.GetById(id);
            return store;
        }

        public IApplicationResponse Save(StockItem stockItem)
        {
            _validator.Validate(stockItem);

            if (!_validator.IsValid)
                return ValidationErrorResponse();

            AddOrUpdate(stockItem);

            return SuccessResponse();
        }

        private IApplicationResponse SuccessResponse()
        {
            return new ApplicationResponse
            {
                IsValid = _validator.IsValid,
                Messages = new List<string> { "Loja salva com sucesso" }
            };
        }

        private IApplicationResponse ValidationErrorResponse()
        {
            return new ApplicationResponse
            {
                IsValid = _validator.IsValid,
                Messages = _validator.ErrorMessages
            };
        }

        private void AddOrUpdate(StockItem stockItem)
        {
            if (stockItem.Id == 0)
                _repository.Add(stockItem);
            else
                _repository.Update(stockItem);
        }

        public void Delete(StockItem stockItem)
        {
            _repository.Delete(stockItem);
        }
    }
}

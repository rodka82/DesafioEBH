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
    public class StoreService : IStoreService
    {
        private readonly IStoreValidator _validator;
        private readonly IStoreRepository _repository;

        public StoreService(IStoreValidator validator, IStoreRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public Store GetById(int id)
        {
            var store = _repository.GetById(id);
            return store;
        }

        public IApplicationResponse Save(Store store)
        {
            _validator.Validate(store);

            if (!_validator.IsValid)
                return ValidationErrorResponse();

            AddOrUpdate(store);

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

        private void AddOrUpdate(Store store)
        {
            if (store.Id == 0)
                _repository.Add(store);
            else
                _repository.Update(store);
        }

        public void Delete(Store store)
        {
            _repository.Delete(store);
        }
    }
}

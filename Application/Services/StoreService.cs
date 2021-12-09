using Application.Utils;
using Application.Validators;
using Domain.Entities;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreValidator _validator;

        public StoreService(IStoreValidator validator)
        {
            _validator = validator;
        }
        public IApplicationResponse Save(Store store)
        {
            _validator.Validate(store);
            if (!_validator.IsValid)
                return new ApplicationResponse
                {
                    IsValid = _validator.IsValid,
                    Messages = _validator.ErrorMessages
                };

            return new ApplicationResponse
            {
                IsValid = _validator.IsValid,
                Messages = new List<string> { "Loja salva com sucesso" }
            };
        }
    }
}

using Application.Utils;
using Application.Validators;
using Domain.Entities;
using Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IValidator<Product> _validator;
        private readonly IRepository<Product> _repository;

        public ProductService(IValidator<Product> validator, IRepository<Product> repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public Product GetById(int id)
        {
            var Product = _repository.GetById(id);
            return Product;
        }

        public IApplicationResponse Save(Product Product)
        {
            _validator.Validate(Product);

            if (!_validator.IsValid)
                return ReturnValidationErrorResponse();

            AddOrUpdate(Product);

            return ReturnSuccessResponse();
        }

        private IApplicationResponse ReturnSuccessResponse()
        {
            return new ApplicationResponse
            {
                IsValid = _validator.IsValid,
                Messages = new List<string> { "Loja salva com sucesso" }
            };
        }

        private IApplicationResponse ReturnValidationErrorResponse()
        {
            return new ApplicationResponse
            {
                IsValid = _validator.IsValid,
                Messages = _validator.ErrorMessages
            };
        }

        private void AddOrUpdate(Product Product)
        {
            if (Product.Id == 0)
                _repository.Add(Product);
            else
                _repository.Update(Product);
        }

        public void Delete(Product Product)
        {
            _repository.Delete(Product);
        }
    }
}

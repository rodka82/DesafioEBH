using Domain.Entities;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Validators
{
    public class ProductValidator : Validator<Product>
    {
        public override void Validate(Product product)
        {
            ValidateName(product);
            ValidatePrice(product);

            SetValidity();
        }

        private void ValidatePrice(Product product)
        {
            if (product.Price <= default(double))
            {
                ErrorMessages.Add("O preço do produto deve ser informado");
            }
        }

        private void ValidateName(Product product)
        {
            if (product.Name.IsNull() || product.Name.IsEmpty())
            {
                ErrorMessages.Add("Informe o Nome do produto.");
            }
        }
    }
}

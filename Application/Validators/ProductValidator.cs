using Domain.Entities;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Validators
{
    public class ProductValidator : IValidator<Product>
    {

        public List<string> ErrorMessages { get; set; }

        public bool IsValid { get; set; }

        public ProductValidator()
        {
            ErrorMessages = new List<string>();
        }

        public void Validate(Product product)
        {
            ValidateName(product);
            ValidatePrice(product);

            IsValid = !ErrorMessages.Any();
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

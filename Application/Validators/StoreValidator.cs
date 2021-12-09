using Application.Utils;
using Domain.Entities;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Validators
{
    public class StoreValidator : IStoreValidator
    {
        public List<string> ErrorMessages { get; set; }

        public bool IsValid { get; set; }

        public StoreValidator()
        {
            ErrorMessages = new List<string>();
        }

        public void Validate(Store store)
        {
            ValidateName(store);
            ValidateAddress(store);

            IsValid = !ErrorMessages.Any();
        }

        private void ValidateAddress(Store store)
        {
            if (store.Address.IsNull())
            {
                ErrorMessages.Add("O Endereço não pode ser nulo");
            }
            else if (store.Address.IsEmpty())
            {
                ErrorMessages.Add("O Endereço não pode ser vazio");
            }
        }

        private void ValidateName(Store store)
        {
            if (store.Name.IsNull())
            {
                ErrorMessages.Add("Nome da loja não pode ser nulo");
            }
            else if (store.Name.IsEmpty())
            {
                ErrorMessages.Add("Nome da loja não pode ser vazio");
            }
        }
    }
}

using Application.Utils;
using Domain.Entities;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Validators
{
    public class StoreValidator : Validator<Store>
    {
        public override void Validate(Store store)
        {
            if (!IsNull(store, "Nenhuma loja foi informada"))
            {
                ValidateName(store);
                ValidateAddress(store);
            }

            SetValidity();
        }

        private void ValidateAddress(Store store)
        {
            if (store.Address.IsNull() || store.Address.IsEmpty())
                ErrorMessages.Add("O endereço deve ser preenchido");
        }

        private void ValidateName(Store store)
        {
            if (store.Name.IsNull() || store.Name.IsEmpty())
                ErrorMessages.Add("O nome da loja deve ser preenchido");
        }
    }
}

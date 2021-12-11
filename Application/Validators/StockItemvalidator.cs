using Application.Utils;
using Domain.Entities;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Validators
{
    public class StockItemValidator : Validator<StockItem>
    {
        public override void Validate(StockItem stockItem)
        {
            ValidateStore(stockItem);
            ValidateProduct(stockItem);

            SetValidity();
        }

        private void ValidateProduct(StockItem stockItem)
        {
            if (stockItem.Product == null)
            {
                ErrorMessages.Add("Um produto deve ser associado a este Item");
            }
        }

        private void ValidateStore(StockItem stockItem)
        {
            if (stockItem.Store == null)
            {
                ErrorMessages.Add("Uma loja deve ser associada a este Item");
            }
        }
    }
}

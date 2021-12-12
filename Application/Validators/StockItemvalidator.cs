using Application.Utils;
using Domain.Entities;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Validators
{
    public class StockItemValidator : Validator<StockItem>, IStockItemValidator
    {
        public override void Validate(StockItem stockItem)
        {
            if (!IsNull(stockItem, "Nenhum item foi informado"))
            {
                ValidateStore(stockItem);
                ValidateProduct(stockItem);
            }

            SetValidity();
        }

        public void Validate(StockOperation operation, StockItem stockItem)
        {
            if(operation.OperationType == StockOperationType.Increment)
                ValidateIncrement(operation);
            else
                ValidateDecrement(operation, stockItem);

            SetValidity();
        }

        private void ValidateIncrement(StockOperation operation)
        {
            if (operation.Quantity < 0)
                ErrorMessages.Add("É necessário informar um valor positivo para a quantidade.");
        }

        private void ValidateDecrement(StockOperation operation, StockItem stockItem)
        {
            if(operation.Quantity > stockItem.Quantity)
                ErrorMessages.Add("Não há quantidade suficiente do produto. Informe um valor menor.");
        }

        private void ValidateProduct(StockItem stockItem)
        {
            if (stockItem.ProductId == 0)
            {
                ErrorMessages.Add("Um produto deve ser associado a este Item");
            }
        }

        private void ValidateStore(StockItem stockItem)
        {
            if (stockItem.StoreId == 0)
            {
                ErrorMessages.Add("Uma loja deve ser associada a este Item");
            }
        }
    }
}

using Domain.Entities;

namespace Application.Validators
{
    public interface IStockItemValidator: IValidator<StockItem>
    {
        void Validate(StockOperation operation, StockItem stockItem);
    }
}
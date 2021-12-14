using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class StockItemDTO : BaseEntityDTO
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

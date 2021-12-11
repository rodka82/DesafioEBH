using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class StockItemDTO : BaseEntityDTO
    {
        public StoreDTO Store { get; set; }
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }
    }
}

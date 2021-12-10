using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class StockItem : BaseEntity
    {
        public Store Store { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public OperationType OperationType { get; set; }
    }
}

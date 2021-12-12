using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class StockItem : BaseEntity
    {
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StockOperation
    {
        public int StockItemId { get; set; }
        public StockOperationType OperationType { get; set; }
        public int Quantity { get; set; }
    }
}

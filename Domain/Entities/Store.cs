using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Store : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}

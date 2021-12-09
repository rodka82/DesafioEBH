using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}

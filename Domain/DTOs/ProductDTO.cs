using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class ProductDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}

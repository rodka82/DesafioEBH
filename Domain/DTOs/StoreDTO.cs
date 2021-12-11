using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class StoreDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}

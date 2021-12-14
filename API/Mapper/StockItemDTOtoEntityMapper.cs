using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mapper
{
    public static class StockItemDTOtoEntityMapper
    {
        public static StockItem ToEntity(this StockItemDTO dto)
        {
            var stockItem = new StockItem();
            stockItem.Id = dto.Id;
            stockItem.ProductId = dto.ProductId;
            stockItem.StoreId = dto.StoreId;
            stockItem.Quantity = dto.Quantity;
            return stockItem;
        }
    }
}
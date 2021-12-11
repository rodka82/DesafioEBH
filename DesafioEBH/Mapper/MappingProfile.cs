using API.Utils;
using Application.Utils;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Store, StoreDTO>();
            CreateMap<StockItem, StockItemDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<ApplicationResponse, ApiResponse>();
        }
    }
}

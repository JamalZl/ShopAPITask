using APIFirstProject.Data.Entities;
using AutoMapper;
using ShopAPIFirst.Apps.AdminApi.Dtos.CategoryDtos;
using ShopAPIFirst.Apps.AdminApi.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAPIFirst.Apps.AdminApi.Profiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryGetDto>();
            CreateMap<Category, CategoryInProductDto>();
            CreateMap<Product, ProductGetDto>()
            .ForMember(dest => dest.Profit, map => map.MapFrom(src => src.SalePrice - src.CostPrice));
        }
    }
}

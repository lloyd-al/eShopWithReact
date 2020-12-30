using AutoMapper;
using eShopWithReact.Services.ProductCatalog.Application.DTOs;
using eShopWithReact.Services.ProductCatalog.Core.Entities;


namespace eShopWithReact.Services.ProductCatalog.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}

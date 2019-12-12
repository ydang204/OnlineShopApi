using AutoMapper;
using OnlineShop.Common.Models.ProductAPI;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ResModels;
using OnlineShop.Common.Utitlities;
using System.Linq;

namespace OnlineShop.ProductAPI.MappingProfiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductReqModel, Product>()
                .ForMember(des => des.SlugName, option => option.MapFrom(src => src.Name.GenerateSlug()))
                .ForMember(des => des.ProductImages, option => option.Ignore());

            CreateMap<Product, ProductDetailsResModel>()
                 .ForMember(des => des.CategoryName, option => option.MapFrom(src => src.Category.Name))
                .ForMember(des => des.BrandName, option => option.MapFrom(src => src.Brand.Name));

            CreateMap<Product, ProductResModel>()
                .ForMember(des => des.CategoryName, option => option.MapFrom(src => src.Category.Name))
                .ForMember(des => des.BrandName, option => option.MapFrom(src => src.Brand.Name));

            CreateMap<Product, SearchProductResModel>()
                .ForMember(des => des.ImageUrl, option => option.MapFrom(src => src.ProductImages.First().ImageUrl))
                .ForMember(des => des.CategoryName, option => option.MapFrom(src => src.Category.Name))
                .ForMember(des => des.BrandName, option => option.MapFrom(src => src.Brand.Name));
        }
    }
}
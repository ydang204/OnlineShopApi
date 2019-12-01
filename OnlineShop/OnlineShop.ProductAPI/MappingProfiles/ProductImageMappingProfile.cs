using AutoMapper;
using CloudinaryDotNet.Actions;
using OnlineShop.Common.Models.ProductAPI;
using OnlineShop.Common.Models.ProductAPI.ResModels;

namespace OnlineShop.ProductAPI.MappingProfiles
{
    public class ProductImageMappingProfile : Profile
    {
        public ProductImageMappingProfile()
        {
            CreateMap<ImageUploadResult, ProductImage>()
                    .ForMember(des => des.ImageUrl, option => option.MapFrom(src => src.Uri.ToString()));
            CreateMap<ProductImage, ProductImageResModel>();
        }
    }
}
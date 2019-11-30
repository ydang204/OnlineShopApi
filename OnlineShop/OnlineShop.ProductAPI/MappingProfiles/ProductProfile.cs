using AutoMapper;
using CloudinaryDotNet.Actions;
using OnlineShop.Common.Models.ProductAPI;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ResModels;
using OnlineShop.Common.Utitlities;

namespace OnlineShop.ProductAPI.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductReqModel, Product>()
                .ForMember(des => des.SlugName, option => option.MapFrom(src => src.Name.GenerateSlug()))
                .ForMember(des => des.ProductImages, option => option.Ignore());

            CreateMap<ImageUploadResult, ProductImage>()
                .ForMember(des => des.ImageUrl, option => option.MapFrom(src => src.Uri.ToString()));

            CreateMap<Product, ProductResModel>();

            CreateMap<ProductImage, ProductImageResModel>();
        }
    }
}
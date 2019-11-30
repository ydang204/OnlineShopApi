using AutoMapper;
using OnlineShop.Common.Models.ProductAPI;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Utitlities;
using System.Linq;

namespace OnlineShop.OrderAPI.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductReqModel, Product>()
                .ForMember(dest => dest.SlugName, option => option.MapFrom(src => src.Name.GenerateSlug()));
        }
    }
}
using AutoMapper;
using OnlineShop.Common.Models.ProductAPI;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ResModels;
using OnlineShop.Common.Utitlities;

namespace OnlineShop.ProductAPI.MappingProfiles
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {
            CreateMap<CreateBrandReqModel, Brand>()
                .ForMember(des => des.SlugName, option => option.MapFrom(src => src.Name.GenerateSlug()));

            CreateMap<Brand, BrandResModel>();
        }
    }
}
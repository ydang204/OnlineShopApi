using AutoMapper;
using OnlineShop.Common.Models.ProductAPI;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ResModels;
using OnlineShop.Common.Utitlities;

namespace OnlineShop.ProductAPI.MappingProfiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CreateCategoryReqModel, Category>().ForMember(des => des.SlugName, option => option.MapFrom(src => src.Name.GenerateSlug()));

            CreateMap<Category, CategoryResModel>();
            CreateMap<Category, ChildCategoryResModel>();
        }
    }
}
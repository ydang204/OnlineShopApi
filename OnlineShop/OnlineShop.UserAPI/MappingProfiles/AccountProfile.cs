using AutoMapper;
using OnlineShop.Common.Models.UserAPI;
using OnlineShop.Common.Models.UserAPI.ReqModels;
using OnlineShop.Common.Models.UserAPI.ResModels;
using OnlineShop.Common.Utitlities;

namespace OnlineShop.UserAPI.MappingProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterReqModel, Account>()
                .ForMember(dest => dest.PasswordHash, option => option.MapFrom(src => src.Password.HashPassword()));

            CreateMap<Account, AccountResModel>();
        }
    }
}
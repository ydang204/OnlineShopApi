using AutoMapper;
using OnlineShop.Common.Utitlities;
using OnlineShop.UserAPI.Models;
using OnlineShop.UserAPI.Models.ReqModels;

namespace OnlineShop.UserAPI.MappingProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterReqModel, Account>()
                .ForMember(dest => dest.PasswordHash, option => option.MapFrom(src => src.Password.HashPassword()));
        }
    }
}
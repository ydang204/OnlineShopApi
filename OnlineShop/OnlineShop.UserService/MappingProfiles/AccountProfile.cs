using AutoMapper;
using OnlineShop.Common.Utitlities;
using OnlineShop.UserService.Models;
using OnlineShop.UserService.Models.ReqModels;

namespace OnlineShop.UserService.MappingProfiles
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
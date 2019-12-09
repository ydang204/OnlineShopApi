using AutoMapper;
using OnlineShop.Common.Models.OrderAPI;
using OnlineShop.Common.Models.OrderAPI.ReqModels.MomoPayment;
using OnlineShop.Common.Models.OrderAPI.ReqModels.Orders;

namespace OnlineShop.OrderAPI.MappingProfiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<CreateOrderReqModel, Order>();

            CreateMap<PaymentReqModel, MoMoPaymentReqModel>();

            CreateMap<Order, PaymentReqModel>()
                .ForMember(des => des.ExtraData, opt => opt.MapFrom(src => src.Email))
                .ForMember(des => des.OrderInfo, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.RequestId, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.Amount, opt => opt.MapFrom(src => src.ToTal));
        }
    }
}
using AutoMapper;
using Microsoft.Extensions.Options;
using OnlineShop.Common.Models.OrderAPI.ReqModels.MomoPayment;
using OnlineShop.Common.SettingOptions;
using System.Threading.Tasks;

namespace OnlineShop.Common.Utitlities
{
    public class MoMoPaymentHelper
    {
        private readonly MoMoSecurity crypto = new MoMoSecurity();

        private readonly ApiRequestHelper _apiRequestHelper;
        private readonly IMapper _mapper;
        private readonly MoMoPaymentOptions _momoOptions;

        public MoMoPaymentHelper(ApiRequestHelper apiRequestHelper, IOptions<MoMoPaymentOptions> momoOptions, IMapper mapper)
        {
            _apiRequestHelper = apiRequestHelper;
            _mapper = mapper;
            _momoOptions = momoOptions.Value;
        }

        public async Task<string> CreatePaymentRequestAync(PaymentReqModel order)
        {
            var body = CreatePaymentSignature(order);
            var response = await _apiRequestHelper.PostAsync<dynamic>(_momoOptions.MoMoPaymentEndpoint, requestBody: body);

            if (response.message == "Success")
            {
                return response.payUrl;
            }

            return response.ToString();
        }

        private MoMoPaymentReqModel CreatePaymentSignature(PaymentReqModel order)
        {
            string rawHash = "partnerCode=" +
            _momoOptions.PartnerCode + "&accessKey=" +
            _momoOptions.AccessKey + "&requestId=" +
            order.RequestId + "&amount=" +
            order.Amount + "&orderId=" +
            order.OrderId + "&orderInfo=" +
            order.OrderInfo + "&returnUrl=" +
            _momoOptions.ReturnUrl + "&notifyUrl=" +
            _momoOptions.NotifyUrl + "&extraData=" +
            order.ExtraData;

            //sign signature SHA256
            string SignatureScripted = crypto.signSHA256(rawHash, _momoOptions.SecrectKey);

            var model = _mapper.Map<PaymentReqModel, MoMoPaymentReqModel>(order);
            model.SetMoMoData(_momoOptions, SignatureScripted);

            return model;
        }
    }
}
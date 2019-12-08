using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Common.Constants;
using OnlineShop.Common.Models.OrderAPI.ReqModels.MomoPayment;
using System.Threading.Tasks;   
using OnlineShop.Common.Models.OrderAPI.ResModels;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System;

namespace OnlineShop.OrderAPI.Controllers
{
    [Route("api/momo-payment")]
    [ApiController]
    [Authorize]
    public class MomoPaymentController : ControllerBase
    {
        public MomoPaymentController()
        {
        }

        [HttpPost]
        [Authorize]
        public string CallPayment([FromBody]PaymentDataReqModel reqModel)
        {
            return sendPaymentRequest(SharedContants.MOMO_ENDPOINT, reqModel.GetDataJsonObject().ToString());
        }

        private string sendPaymentRequest(string endpoint, string postJsonString)
        {
            try
            {
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(endpoint);

                var postData = postJsonString;

                var data = Encoding.UTF8.GetBytes(postData);

                httpWReq.ProtocolVersion = HttpVersion.Version11;
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/json";

                httpWReq.ContentLength = data.Length;
                httpWReq.ReadWriteTimeout = 30000;
                httpWReq.Timeout = 15000;
                Stream stream = httpWReq.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();

                string jsonresponse = "";

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                }
                Console.WriteLine(jsonresponse);
                //todo parse it
                return jsonresponse;
                //return new MomoResponse(mtid, jsonresponse);
            }
            catch (WebException e)
            {
                return e.Message;
            }
        }

        [HttpPost("ipn")]
        public async Task<IActionResult> MomoPaymentNotification([FromForm]MomoIpnResModel model)
        {
            return Ok(model);
        }
    }
}
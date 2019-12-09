using Newtonsoft.Json;
using OnlineShop.Common.Models.Notification.ReqModels;
using OnlineShop.Common.SettingOptions;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Common.Utitlities
{
    public static class NotificationHelper
    {
        /// <summary>
        /// Send noti to fcm
        /// </summary>
        /// <param name="fcmOption"></param>
        /// <param name="notiModel"></param>
        /// <returns></returns>
        public static async Task SendNotiAsync(FcmProviderOptions fcmOption, NotificationContent notiModel, string token)
        {
            try
            {
                /* request */
                WebRequest tRequest = WebRequest.Create(fcmOption.ApiUrl);
                tRequest.Method = "post";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", fcmOption.LegacyServerKey));
                tRequest.ContentType = "application/json";

                /* data */
                var payload = new
                {
                    to = token,
                    priority = 10, // 10 is better than high priority
                    content_available = true,
                    notification = new
                    {
                        title = notiModel.Title,
                        body = notiModel.Body,
                        icon = "logo512.png",
                        click_action = fcmOption.ClientSideUrl + notiModel.ActionUrl,
                    },
                    //data = new
                    //{
                    //    dataId = notiModel.DataId,
                    //    type = notiModel.NotificationType.ToString()
                    //}
                };

                /* send to fcm */
                string postbody = JsonConvert.SerializeObject(payload).ToString();
                Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = await tRequest.GetResponseAsync())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    string sResponseFromServer = tReader.ReadToEnd();
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
using Microsoft.AspNetCore.Http;


namespace OnlineShop.Common.Models.ProductAPI.ReqModels
{
    public class UploadPhotoReqModel
    {
        public IFormFile File { get; set; }
    }
}
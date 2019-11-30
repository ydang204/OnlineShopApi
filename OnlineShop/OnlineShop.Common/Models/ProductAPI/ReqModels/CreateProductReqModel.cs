using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace OnlineShop.Common.Models.ProductAPI.ReqModels
{
    public class CreateProductReqModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public List<IFormFile> ProductImages { get; set; }
    }
}
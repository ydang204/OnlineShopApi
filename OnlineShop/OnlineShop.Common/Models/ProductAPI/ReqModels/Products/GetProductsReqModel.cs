using OnlineShop.Common.Models.Common.ReqModels;

namespace OnlineShop.Common.Models.ProductAPI.ReqModels.Products
{
    public class GetProductsReqModel : BasePagingRequest
    {
        public int? CategoryId { get; set; }

        public int? BrandId { get; set; }
        public string Name { get; set; }
    }
}
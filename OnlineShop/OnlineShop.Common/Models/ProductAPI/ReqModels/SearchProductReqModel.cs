namespace OnlineShop.Common.Models.ProductAPI.ReqModels
{
    public class SearchProductReqModel
    {
        public int? CategoryId { get; set; }

        public int? BrandId { get; set; }

        public string Name { get; set; }
    }
}
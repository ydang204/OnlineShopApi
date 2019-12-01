namespace OnlineShop.Common.Models.ProductAPI.ResModels
{
    public class SearchProductResModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string BrandName { get; set; }

        public string ImageUrl { get; set; }

        public string SlugName { get; set; }
    }
}
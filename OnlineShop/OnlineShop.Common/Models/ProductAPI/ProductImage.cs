namespace OnlineShop.Common.Models.ProductAPI
{
    public class ProductImage : BaseEntity<int>
    {
        public string ImageUrl { get; set; }

        public int ProductId { get; set; }

        public string PublicId { get; set; }

        public Product Product { get; set; }
    }
}
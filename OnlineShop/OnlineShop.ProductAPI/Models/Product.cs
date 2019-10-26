using OnlineShop.Common.Models;

namespace OnlineShop.ProductAPI.Models
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string SlugName { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
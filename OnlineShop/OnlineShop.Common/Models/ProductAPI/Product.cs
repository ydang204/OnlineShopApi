using System.Collections.Generic;

namespace OnlineShop.Common.Models.ProductAPI
{
    public class Product : BaseEntity<int>
    {
        public Product()
        {
            ProductImages = new List<ProductImage>();
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string SlugName { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
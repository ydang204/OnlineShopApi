using System.Collections.Generic;

namespace OnlineShop.Common.Models.ProductAPI.ResModels
{
    public class ProductResModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string SlugName { get; set; }

        public CategoryResModel Category { get; set; }

        public BrandResModel Brand { get; set; }

        public ICollection<ProductImageResModel> ProductImages { get; set; }
    }
}
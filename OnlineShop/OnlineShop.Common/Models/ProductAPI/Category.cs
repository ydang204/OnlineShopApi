using System.Collections.Generic;

namespace OnlineShop.Common.Models.ProductAPI
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }

        public string SlugName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
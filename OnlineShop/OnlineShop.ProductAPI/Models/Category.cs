using OnlineShop.Common.Models;
using System.Collections.Generic;

namespace OnlineShop.ProductAPI.Models
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }

        public string SlugName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
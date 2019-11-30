using System.Collections.Generic;

namespace OnlineShop.Common.Models.ProductAPI
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }

        public string SlugName { get; set; }

        public int? ParentId { get; set; }

        public Category Parent { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
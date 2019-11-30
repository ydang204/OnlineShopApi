using System.Collections.Generic;

namespace OnlineShop.Common.Models.ProductAPI.ResModels
{
    public class CategoryResModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SlugName { get; set; }

        public List<CategoryResModel> ChildCategories { get; set; }
    }
}
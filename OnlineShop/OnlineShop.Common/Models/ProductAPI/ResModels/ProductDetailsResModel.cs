namespace OnlineShop.Common.Models.ProductAPI.ResModels
{
    public class ProductDetailsResModel : ProductResModel
    {
        public BrandResModel Brand { get; set; }

        public ChildCategoryResModel Category { get; set; }
    }
}
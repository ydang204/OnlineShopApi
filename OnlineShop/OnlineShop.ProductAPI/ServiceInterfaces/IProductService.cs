using OnlineShop.Common.Models.Common.ReqModels;
using OnlineShop.Common.Models.Common.ResModels;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ReqModels.Products;
using OnlineShop.Common.Models.ProductAPI.ResModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.ProductAPI.ServiceInterfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(CreateProductReqModel model);
        Task<BasePagingResponse<ProductResModel>> GetProductsAsync(GetProductsReqModel model);
        Task<List<SearchProductResModel>> SearchProductsAsync(SearchProductReqModel model);
        Task<ProductDetailsResModel> GetProductDetailsAsync(int id);
        Task<ProductDetailsResModel> GetProductDetailsAsync(string slug);
        Task<List<ProductResModel>> GetProductByIdsAsync(List<int> ids);
    }
}
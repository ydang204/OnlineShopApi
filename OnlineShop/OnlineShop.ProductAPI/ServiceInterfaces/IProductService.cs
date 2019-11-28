using OnlineShop.Common.Models.Common.ReqModels;
using OnlineShop.Common.Models.Common.ResModels;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ResModels;
using System.Threading.Tasks;

namespace OnlineShop.ProductAPI.ServiceInterfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(CreateProductReqModel model);
        Task<BasePagingResponse<ProductResModel>> GetProductsAsync(BasePagingRequest model);
    }
}
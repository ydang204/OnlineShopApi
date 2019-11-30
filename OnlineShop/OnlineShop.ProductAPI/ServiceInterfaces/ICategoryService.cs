using OnlineShop.Common.Models.Common.ResModels;
using OnlineShop.Common.Models.ProductAPI;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ResModels;
using System.Threading.Tasks;

namespace OnlineShop.ProductAPI.ServiceInterfaces
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(Category brand);

        Task<BasePagingResponse<CategoryResModel>> GetCategoriesAsync(GetBrandsReqModel model);
    }
}
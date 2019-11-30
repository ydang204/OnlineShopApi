using OnlineShop.Common.Models.Common.ResModels;
using OnlineShop.Common.Models.ProductAPI;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ResModels;
using OnlineShop.ProductAPI.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.ProductAPI.Services
{
    public class CategoryService : ICategoryService
    {
        public Task CreateCategoryAsync(Category brand)
        {
            throw new NotImplementedException();
        }

        public Task<BasePagingResponse<CategoryResModel>> GetCategoriesAsync(GetBrandsReqModel model)
        {
            throw new NotImplementedException();
        }
    }
}

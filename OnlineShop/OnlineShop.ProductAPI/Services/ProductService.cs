using OnlineShop.Common.Models.Common.ReqModels;
using OnlineShop.Common.Models.Common.ResModels;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ResModels;
using OnlineShop.ProductAPI.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.ProductAPI.Services
{
    public class ProductService : IProductService
    {
        public Task CreateProductAsync(CreateProductReqModel model)
        {
            throw new NotImplementedException();
        }

        public Task<BasePagingResponse<ProductResModel>> GetProductsAsync(BasePagingRequest model)
        {
            throw new NotImplementedException();
        }
    }
}

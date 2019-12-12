using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Common.Models.Common.ResModels;
using OnlineShop.Common.Models.ProductAPI;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ResModels;

namespace OnlineShop.ProductAPI.ServiceInterfaces
{
    public interface IBrandService
    {
        Task CreateBrandAsync(Brand brand);
        Task<BasePagingResponse<BrandResModel>> GetBrandsAsync(GetBrandsReqModel model);
    }
}

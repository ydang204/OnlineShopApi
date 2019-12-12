using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Common.Constants;
using OnlineShop.Common.Models.Common.ResModels;
using OnlineShop.Common.Models.ProductAPI;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ResModels;
using OnlineShop.ProductAPI.ServiceInterfaces;
using System.Threading.Tasks;

namespace OnlineShop.ProductAPI.Controllers
{
    [Route(SharedContants.API_V1_SPEC)]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public BrandsController(IBrandService brandService, IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<BasePagingResponse<BrandResModel>> GetBrands([FromQuery]GetBrandsReqModel model)
        {
            return await _brandService.GetBrandsAsync(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBrand(CreateBrandReqModel model)
        {
            var brand = _mapper.Map<CreateBrandReqModel, Brand>(model);
            await _brandService.CreateBrandAsync(brand);
            return Ok(new { createdBrand = true });
        }
    }
}
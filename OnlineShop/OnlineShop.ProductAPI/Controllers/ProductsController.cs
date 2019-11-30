using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Common.Models.Common.ReqModels;
using OnlineShop.Common.Models.Common.ResModels;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ResModels;
using OnlineShop.ProductAPI.ServiceInterfaces;
using System.Threading.Tasks;

namespace OnlineShop.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService,
                                  IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm]CreateProductReqModel reqModel)
        {
            await _productService.CreateProductAsync(reqModel);
            return Ok(new { createProductSucceed = true });
        }


        [HttpGet]
        public async Task<BasePagingResponse<ProductResModel>> GetProduct([FromQuery]BasePagingRequest model)
        {
            return await _productService.GetProductsAsync(model);
        }
    }
}
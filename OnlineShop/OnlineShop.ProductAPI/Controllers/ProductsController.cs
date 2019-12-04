using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Common.Models.Common.ReqModels;
using OnlineShop.Common.Models.Common.ResModels;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ReqModels.Products;
using OnlineShop.Common.Models.ProductAPI.ResModels;
using OnlineShop.ProductAPI.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
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
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromForm]CreateProductReqModel reqModel)
        {
            await _productService.CreateProductAsync(reqModel);
            return Ok(new { createProductSucceed = true });
        }

        [HttpGet("{id}")]
        public async Task<ProductDetailsResModel> ProductDetails(int id)
        {
            return await _productService.GetProductDetailsAsync(id);
        }


        [HttpGet("slug/{slug}")]
        public async Task<ProductDetailsResModel> ProductDetails(string slug)
        {
            return await _productService.GetProductDetailsAsync(slug);
        }


        [HttpGet]
        public async Task<BasePagingResponse<ProductResModel>> GetProduct([FromQuery]GetProductsReqModel model)
        {
            return await _productService.GetProductsAsync(model);
        }

        [HttpGet("search")]
        public async Task<List<SearchProductResModel>> SearchProduct([FromQuery]SearchProductReqModel model)
        {
            return await _productService.SearchProductsAsync(model);
        }
    }
}
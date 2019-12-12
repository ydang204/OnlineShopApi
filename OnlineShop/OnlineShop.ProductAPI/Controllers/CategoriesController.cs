using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Common.Constants;
using OnlineShop.Common.Models.Common.ResModels;
using OnlineShop.Common.Models.ProductAPI;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ResModels;
using OnlineShop.ProductAPI.ServiceInterfaces;

namespace OnlineShop.ProductAPI.Controllers
{
    [Route(SharedContants.API_V1_SPEC)]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<BasePagingResponse<CategoryResModel>> GetBrands([FromQuery]GetBrandsReqModel model)
        {
            return await _categoryService.GetCategoriesAsync(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCategory(CreateCategoryReqModel model)
        {
            var category = _mapper.Map<CreateCategoryReqModel, Category>(model);
            await _categoryService.CreateCategoryAsync(category);
            return Ok(new { createdBrand = true });
        }
    }
}
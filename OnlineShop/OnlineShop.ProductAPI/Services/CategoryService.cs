using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Common.Exceptions;
using OnlineShop.Common.Models.Common.ResModels;
using OnlineShop.Common.Models.ProductAPI;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ResModels;
using OnlineShop.Common.Resources;
using OnlineShop.Common.Utitlities;
using OnlineShop.ProductAPI.Models;
using OnlineShop.ProductAPI.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.ProductAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ProductContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(Category category)
        {
            var existCategory = await _context.Categories.FirstOrDefaultAsync(b => b.Name.ToLower().Equals(category.Name.ToLower()));

            if (existCategory != null)
            {
                throw new CustomException(Errors.CATEGORY_ALREADY_EXIST, Errors.CATEGORY_ALREADY_EXIST_MSG);
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<BasePagingResponse<CategoryResModel>> GetCategoriesAsync(GetBrandsReqModel model)
        {
            var result = new BasePagingResponse<CategoryResModel>();

            var query = _context.Categories.AsNoTracking();

            query = CommonFunctions.SortQuery(model, query);

            result.Total = await query.CountAsync();
            if (model.Page.HasValue && model.Page >= 0 && model.PageSize.HasValue && model.PageSize > 0)
            {
                query = query.Skip(model.PageSize.Value * (model.Page.Value - 1)).Take(model.PageSize.Value);
            }

            var categories = await query.ToListAsync();

            // Get all parent categories
            var parentCategories = categories.Where(c => !c.ParentId.HasValue).ToList();
            result.Items = _mapper.Map<List<Category>, List<CategoryResModel>>(parentCategories);

            // Get all child categories
            foreach (var category in result.Items)
            {
                var childCategories = categories.Where(c => c.ParentId == category.Id).ToList();
                category.ChildCategories = _mapper.Map<List<Category>, List<CategoryResModel>>(childCategories);
            }

            result.Page = model.Page;
            result.PageSize = model.PageSize;
            result.TotalPage = (int)Math.Ceiling(result.Total / (double)result.PageSize);

            return result;
        }
    }
}
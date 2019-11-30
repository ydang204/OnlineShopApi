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
    public class BrandService : IBrandService
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;

        public BrandService(ProductContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateBrandAsync(Brand brand)
        {
            var existBrand = await _context.Brands.FirstOrDefaultAsync(b => b.Name.ToLower().Equals(brand.Name.ToLower()));

            if (existBrand != null)
            {
                throw new CustomException(Errors.BRAND_ALREADY_EXIST, Errors.BRAND_ALREADY_EXIST_MSG);
            }

            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
        }

        public async Task<BasePagingResponse<BrandResModel>> GetBrandsAsync(GetBrandsReqModel model)
        {
            var result = new BasePagingResponse<BrandResModel>();

            var query = _context.Brands.AsNoTracking();

            query = CommonFunctions.SortQuery(model, query);

            result.Total = await query.CountAsync();
            if (model.Page.HasValue && model.Page >= 0 && model.PageSize.HasValue && model.PageSize > 0)
            {
                query = query.Skip(model.PageSize.Value * (model.Page.Value - 1)).Take(model.PageSize.Value);
            }

            var brands = await query.ToListAsync();
            result.Items = _mapper.Map<List<Brand>, List<BrandResModel>>(brands);
            result.Page = model.Page;
            result.PageSize = model.PageSize;
            result.TotalPage = (int)Math.Ceiling(result.Total / (double)result.PageSize);

            return result;
        }
    }
}
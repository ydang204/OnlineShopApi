using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Common.Exceptions;
using OnlineShop.Common.Models.Common.ReqModels;
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
    public class ProductService : IProductService
    {
        private readonly ProductContext _context;
        private readonly CloudinaryHelper _cloudinaryHelper;
        private readonly IMapper _mapper;

        public ProductService(ProductContext context,
                              IMapper mapper,
                              CloudinaryHelper cloudinaryHelper)
        {
            _context = context;
            _cloudinaryHelper = cloudinaryHelper;
            _mapper = mapper;
        }

        public async Task CreateProductAsync(CreateProductReqModel model)
        {
            var existProduct = await _context.Products
                                             .FirstOrDefaultAsync(p => p.Name.ToLower().Equals(model.Name.ToLower()));

            if (existProduct != null)
            {
                throw new CustomException(Errors.PRODUCT_ALREADY_EXIST, Errors.PRODUCT_ALREADY_EXIST_MSG);
            }

            var uploadResults = _cloudinaryHelper.UploadImages(model.ProductImages);

            var product = _mapper.Map<CreateProductReqModel, Product>(model);

            product.ProductImages = _mapper.Map<List<ImageUploadResult>, List<ProductImage>>(uploadResults);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<BasePagingResponse<ProductResModel>> GetProductsAsync(BasePagingRequest model)
        {
            var result = new BasePagingResponse<ProductResModel>();

            var query = _context.Products.Include(p => p.Category)
                                         .Include(p => p.Brand)
                                         .Include(p => p.ProductImages)
                                         .AsNoTracking();

            query = CommonFunctions.SortQuery(model, query);

            result.Total = await query.CountAsync();
            if (model.Page.HasValue && model.Page >= 0 && model.PageSize.HasValue && model.PageSize > 0)
            {
                query = query.Skip(model.PageSize.Value * (model.Page.Value - 1)).Take(model.PageSize.Value);
            }

            var products = await query.ToListAsync();

            result.Items = _mapper.Map<List<Product>, List<ProductResModel>>(products);
            result.Page = model.Page;
            result.PageSize = model.PageSize;
            result.TotalPage = (int)Math.Ceiling(result.Total / (double)result.PageSize);

            return result;
        }
    }
}
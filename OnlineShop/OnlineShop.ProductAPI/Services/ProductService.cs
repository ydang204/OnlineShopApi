using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Common.Exceptions;
using OnlineShop.Common.Models.Common.ResModels;
using OnlineShop.Common.Models.ProductAPI;
using OnlineShop.Common.Models.ProductAPI.ReqModels;
using OnlineShop.Common.Models.ProductAPI.ReqModels.Products;
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
        private readonly ILogger<ProductService> _logger;

        public ProductService(ProductContext context,
                              IMapper mapper,
                              ILogger<ProductService> logger,
                              CloudinaryHelper cloudinaryHelper)
        {
            _context = context;
            _cloudinaryHelper = cloudinaryHelper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateProductAsync(CreateProductReqModel model)
        {
            var existProduct = await _context.Products
                                             .FirstOrDefaultAsync(p => p.Name.ToLower().Equals(model.Name.ToLower()) ||
                                                p.Name.GenerateSlug().Equals(model.Name.GenerateSlug())
                                             );

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

        public async Task<List<ProductResModel>> GetProductByIdsAsync(List<int> ids)
        {
            var products = await _context.Products
                                         .Include(p => p.Category)
                                         .Include(p => p.Brand)
                                         .Include(p => p.ProductImages)
                                         .Where(p => ids.Contains(p.Id))
                                         .ToListAsync();

            return _mapper.Map<List<Product>, List<ProductResModel>>(products);
        }

        public async Task<ProductDetailsResModel> GetProductDetailsAsync(int id)
        {
            var product = await _context.Products
                                        .Include(p => p.Brand)
                                        .Include(p => p.Category)
                                        .Include(p => p.ProductImages)
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new CustomException(Errors.PRODUCT_NOT_FOUND, Errors.PRODUCT_NOT_FOUND_MSG);
            }

            return _mapper.Map<Product, ProductDetailsResModel>(product);
        }

        public async Task<ProductDetailsResModel> GetProductDetailsAsync(string slug)
        {
            var product = await _context.Products
                            .Include(p => p.Brand)
                            .Include(p => p.Category)
                            .Include(p => p.ProductImages)
                            .FirstOrDefaultAsync(p => p.SlugName == slug);

            if (product == null)
            {
                throw new CustomException(Errors.PRODUCT_NOT_FOUND, Errors.PRODUCT_NOT_FOUND_MSG);
            }

            return _mapper.Map<Product, ProductDetailsResModel>(product);
        }

        public async Task<BasePagingResponse<ProductResModel>> GetProductsAsync(GetProductsReqModel model)
        {
            var result = new BasePagingResponse<ProductResModel>();

            var query = _context.Products.Include(p => p.Category)
                                         .Include(p => p.Brand)
                                         .Include(p => p.ProductImages)
                                         .AsNoTracking();

            if (model.BrandId.HasValue)
            {
                query = query.Where(p => p.BrandId == model.BrandId);
            }

            if (model.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == model.CategoryId);
            }

            if (!string.IsNullOrEmpty(model.Name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(model.Name.ToLower()));
            }

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

        public async Task<List<SearchProductResModel>> SearchProductsAsync(SearchProductReqModel model)
        {
            var query = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(model.Name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(model.Name.Trim().ToLower()));
            }

            if (model.BrandId.HasValue)
            {
                query = query.Where(p => p.BrandId == model.BrandId);
            }

            if (model.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == model.CategoryId);
            }

            var products = await query.ToListAsync();

            return _mapper.Map<List<Product>, List<SearchProductResModel>>(products);
        }
    }
}
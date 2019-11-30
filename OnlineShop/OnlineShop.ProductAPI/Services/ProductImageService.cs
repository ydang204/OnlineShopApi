using OnlineShop.ProductAPI.Models;
using OnlineShop.ProductAPI.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.ProductAPI.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly ProductContext _context;

        public ProductImageService(ProductContext context)
        {
            _context = context;
        }
    }
}

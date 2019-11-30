using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.ProductAPI.Controllers
{
    [Route("api/v1/product-images")]
    [ApiController]
    [Authorize]
    public class ProductImagesController : ControllerBase
    {


    }
}
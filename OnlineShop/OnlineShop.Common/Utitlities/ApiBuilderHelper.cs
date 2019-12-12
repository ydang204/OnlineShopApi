using Microsoft.Extensions.Options;
using OnlineShop.Common.SettingOptions;

namespace OnlineShop.Common.Utitlities
{
    public class ApiBuilderHelper
    {
        private readonly SiteMapUrlOptions _siteMapUrl;

        public ApiBuilderHelper(IOptions<SiteMapUrlOptions> options)
        {
            _siteMapUrl = options.Value;
        }

        /// <summary>
        /// Return product api url
        /// Example: http://localhost:9000/api/v1/product/products
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public string GetProducUrl(string endpoint)
        {
            return _siteMapUrl + "api/v1/product/" + endpoint;
        }
    }
}
using System.Collections.Generic;

namespace OnlineShop.Common.Models.Common.ReqModels
{
    public class BasePagingRequest
    {
        public int? Page { get; set; } = 1;

        public int? PageSize { get; set; } = 10;

        public List<string> SortNames { get; set; }

        public List<OrderDirection> SortDirections { get; set; }
    }

    public enum OrderDirection
    {
        OrderBy, OrderByDescending
    }
}
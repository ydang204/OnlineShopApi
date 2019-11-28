using System.Collections.Generic;

namespace OnlineShop.Common.Models.Common.ResModels
{
    public class BasePagingResponse<T>
    {
        public BasePagingResponse()
        {
            Items = new List<T>();
        }

        public List<T> Items { get; set; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }

        public int Total { get; set; }

        public int TotalPage { get; set; }
    }
}
﻿namespace OnlineShop.Common.Models.ProductAPI.ReqModels
{
    public class CreateCategoryReqModel
    {
        public string Name { get; set; }

        public int? ParentId { get; set; }
    }
}
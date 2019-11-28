using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Common.Models.OrderAPI
{
    public class Order : BaseEntity<int>
    {
        public int UserId { get; set; }

        public int ToTal { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}

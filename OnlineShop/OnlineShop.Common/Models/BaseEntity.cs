using OnlineShop.Common.Enums;
using System;

namespace OnlineShop.Common.Models
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public ObjectStatus ObjectStatus { get; set; }
    }
}
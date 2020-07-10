using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class ShoppingItemVM
    {
        public Guid ShoppingID { get; set; }
        public Product Product { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public List<AttributeJsonVM> Attribute { get; set; }
        public string DiscountName { get; set; }
        public decimal DiscountAmount { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}

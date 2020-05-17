using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class ShoppingItemVM
    {
        public Guid ShoppingID { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public List<AttributeJsonVM> Attribute { get; set; }
        public string DiscountName { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool HasDiscountsApplied { get; set; }
        public DiscountType DiscountType { get; set; }
        public DiscountType SelfProductDiscountType { get; set; }
        public decimal SelfProductDiscountAmount { get; set; }
        public bool HasDiscount { get; set; }
    }
}

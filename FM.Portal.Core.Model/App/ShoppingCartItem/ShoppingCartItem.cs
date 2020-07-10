﻿using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class ShoppingCartItem : Entity
    {
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
        public Guid ShoppingID { get; set; }
        public int Quantity { get; set; }
        public string AttributeJson { get; set; }

        // only show
        public string DiscountName { get; set; }
        public decimal DiscountAmount { get; set; }
        public DiscountType DiscountType { get; set; }
        public Product Product { get; set; }
    }
}

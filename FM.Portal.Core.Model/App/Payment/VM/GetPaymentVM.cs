using FM.Portal.BaseModel;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class PaymentDetailVM:Entity
    {
        public List<Product> Products { get; set; }
        public User User { get; set; }
        public List<AttributeJsonVM> Attributes { get; set; }
        public UserAddress UserAddress { get; set; }
        public Payment Payment { get; set; }

    }
}

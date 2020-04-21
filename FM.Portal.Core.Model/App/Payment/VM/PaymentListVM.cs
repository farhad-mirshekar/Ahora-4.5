using FM.Portal.BaseModel;
using System;
using System.Collections.Generic;
namespace FM.Portal.Core.Model
{
   public class PaymentListVM:Entity
    {
        public string BuyerInfo { get; set; }
        public string BuyerPhone { get; set; }
        //public ResCode ResCode { get; set; }
        public decimal Price { get; set; }
    }
}

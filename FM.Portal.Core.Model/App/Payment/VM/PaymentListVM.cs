using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
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
        public int CountBuy { get; set; }
        public BankName BankName { get; set; }
        public string BankNameString => BankName.ToString().Replace('_',' ');
        public string CreationDatePersian => Helper.GetPersianDate(CreationDate);
    }
}

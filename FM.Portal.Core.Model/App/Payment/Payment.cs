using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;

namespace FM.Portal.Core.Model
{
   public class Payment:Entity
    {
        public Guid UserID { get; set; }
        public Guid OrderID { get; set; }
        public int TransactionStatus { get; set; }
        public string TransactionStatusMessage { get; set; }
        public decimal Price { get; set; }
        public string Token { get; set; }
        public string RetrivalRefNo { get; set; }
        public string SystemTraceNo { get; set; }
        public string CreationDatePersian =>Helper.GetPersianDate(CreationDate);

    }
}

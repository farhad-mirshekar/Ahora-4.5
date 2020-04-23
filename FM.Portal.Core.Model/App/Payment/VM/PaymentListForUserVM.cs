using FM.Portal.BaseModel;
using FM.Portal.Core.Common;

namespace FM.Portal.Core.Model
{
   public class PaymentListForUserVM : Entity
    {
        public string TrackingCode { get; set; }
        public string Price { get; set; }
        public string CreationDatePersian => Helper.GetPersianDate(CreationDate);
    }
}

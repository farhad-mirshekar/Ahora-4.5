using FM.Portal.BaseModel;
using FM.Portal.Core.Common;

namespace FM.Portal.Core.Model
{
   public class Discount : Entity
    {
        public string Name { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }

        //only show
        public string CreationDatePersian => Helper.GetPersianDate(CreationDate);
        public int Total { get; set; }
    }
}

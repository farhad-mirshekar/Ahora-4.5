using FM.Portal.BaseModel;
using FM.Portal.Core.Common;

namespace FM.Portal.Core.Model
{
   public class ProductAttribute : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //only show
        public string CreationDatePersian => Helper.GetPersianDate(CreationDate);
        public int Total { get; set; }
    }
}

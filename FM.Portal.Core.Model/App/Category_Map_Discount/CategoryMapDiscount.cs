using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class CategoryMapDiscount:Entity
    {
        public Guid CategoryID { get; set; }
        public Guid DiscountID { get; set; }
        public bool Active { get; set; }
    }
}

using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class Category : Entity
    {
        public string Title { get; set; }
        public Guid ParentID { get; set; }
        public bool IncludeInTopMenu { get; set; }
        public bool IncludeInLeftMenu { get; set; }
        public bool HasDiscountsApplied { get; set; }
        public Guid DiscountID { get; set; }
    }
}

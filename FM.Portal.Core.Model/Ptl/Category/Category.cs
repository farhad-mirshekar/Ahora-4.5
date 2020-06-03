using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;

namespace FM.Portal.Core.Model.Ptl
{
   public class Category : Entity
    {
        public string Title { get; set; }
        public Guid ParentID { get; set; }
        public string Node { get; set; }
        public string ParentNode { get; set; }
        public bool IncludeInTopMenu { get; set; }
        public bool IncludeInLeftMenu { get; set; }

        //only show
        public string CreationDatePersian => Helper.GetPersianDate(CreationDate);
    }
}

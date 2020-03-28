using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model.Ptl
{
   public class Category : Entity
    {
        public string Title { get; set; }
        public Guid ParentID { get; set; }
        public bool IncludeInTopMenu { get; set; }
        public bool IncludeInLeftMenu { get; set; }
    }
}

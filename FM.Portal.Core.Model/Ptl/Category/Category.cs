using FM.Portal.BaseModel;
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
        public Guid? RemoverID { get; set; }
        public DateTime? RemoverDate { get; set; }
        public string TitleCrumb { get; set; }
    }
}

using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class TagsListVM:Pagination
    {
        public Guid? DocumentID { get; set; }
        public string TagName { get; set; }
    }
}

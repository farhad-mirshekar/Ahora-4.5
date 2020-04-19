using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class TagsSearchListVM:Entity
    {
        public Guid DocumentID { get; set; }
        public DocumentTypeForTags DocumentType { get; set; }
    }
}

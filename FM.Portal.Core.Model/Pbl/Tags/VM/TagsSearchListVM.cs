using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class TagsSearchListVM:Entity
    {
        public Guid DocumentID { get; set; }
        public DocumentTypeForTags DocumentType { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentUrlDesc { get; set; }
        public string TrackingCode { get; set; }
    }
}

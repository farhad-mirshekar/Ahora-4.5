using FM.Portal.BaseModel;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model.Ptl
{
   public class DynamicPages:Entity
    {
        public string TrackingCode { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public Guid PageID { get; set; }
        public string MetaKeyword { get; set; }
        public int VisitedCount { get; set; }
        public bool IsShow { get; set; }
        public Guid UserID { get; set; }
        public List<String> Tags { get; set; }
        public string UrlDesc { get; set; }
    }
}

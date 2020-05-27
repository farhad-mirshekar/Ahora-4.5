using FM.Portal.BaseModel;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class DynamicPage:Entity
    {
        public string Name { get; set; }
        public string TrackingCode { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public Guid PageID { get; set; }
        public string MetaKeywords { get; set; }
        public int VisitedCount { get; set; }
        public EnableMenuType IsShow { get; set; }
        public Guid UserID { get; set; }
        public List<String> Tags { get; set; }
        public string UrlDesc { get; set; }

        //show only
        public string PageName { get; set; }
    }
}

using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class DynamicPage:Entity
    {
        public DynamicPage()
        {
            Tags = new List<string>();
        }
        public string Name { get; set; }
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
        public string FileName { get; set; }
        public string Path => "Pages";
        public int Total { get; set; }
    }
}

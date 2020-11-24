using FM.Portal.BaseModel;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class StaticPage:Entity
    {
        public string Body { get; set; }
        public string Description { get; set; }
        public string MetaKeywords { get; set; }
        public int VisitedCount { get; set; }
        public List<String> Tags { get; set; }
        public EnableMenuType BannerShow { get; set; }
        public Guid AttachmentID { get; set; }

        //only show
        public string PageName { get; set; }
        public Model.Ptl.Pages Pages { get; set; }
    }
}

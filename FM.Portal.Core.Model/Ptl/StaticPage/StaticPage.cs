using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
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
        public string Name { get; set; }
        public string UrlDesc { get; set; }
        public string FileName { get; set; }
        public PathType PathType { get; set; }
        public EnableMenuType Enabled { get; set; }
        public Guid UserID { get; set; }
        public string Path => PathType.ToString();
        public string CreationDatePersian => Helper.GetPersianDate(CreationDate);
        public string TrackingCode { get; set; }
    }
}

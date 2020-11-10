using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class Gallery:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public EnableMenuType Enabled { get; set; }
        public int VisitedCount { get; set; }
        public Guid UserID { get; set; }
        public string TrackingCode { get; set; }
        public List<String> Tags { get; set; }
        public string MetaKeywords { get; set; }
        public string UrlDesc { get; set; }

        //only show
        public string FileName { get; set; }
        public PathType PathType { get; set; }

        public string Path => PathType.ToString();
        public int Total { get; set; }
        public string CreatorName { get; set; }
    }
}

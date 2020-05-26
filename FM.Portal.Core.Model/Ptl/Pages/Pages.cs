using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model.Ptl
{
   public class Pages:Entity
    {
        public string TrackingCode { get; set; }
        public string Name { get; set; }
        public PageType PageType { get; set; }
        public EnableMenuType Enabled { get; set; }
        public Guid UserID { get; set; }
        public string UrlDesc { get; set; }
    }
}

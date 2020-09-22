using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
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

        //only show
        public string CreationDatePersian => Helper.GetPersianDate(CreationDate);
        public int Total { get; set; }
    }
}

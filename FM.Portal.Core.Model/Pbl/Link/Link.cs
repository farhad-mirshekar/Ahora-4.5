using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;

namespace FM.Portal.Core.Model
{
   public class Link:Entity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string IconText { get; set; }
        public bool ShowFooter { get; set; }
        public EnableMenuType Enabled { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public Guid UserID { get; set; }

        //only show
        public string CreationDatePersian => Helper.GetPersianDate(CreationDate);
    }
}

using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;

namespace FM.Portal.Core.Model
{
   public class Notification:Entity
    {
        public Guid UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReadDate { get; set; }
        //only show
        public string CreationDatePersian => Helper.GetPersianDate(CreationDate);
        public string ReadDatePersian
        {
            get
            {
                if (ReadDate != null)
                    return Helper.GetPersianDate(CreationDate);
                else
                    return null;
            }
        }
    }
}

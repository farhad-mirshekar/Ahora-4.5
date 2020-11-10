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
        public Guid PositionID { get; set; }

        //only show
        public string ReadDatePersian
        {
            get
            {
                if (ReadDate != null)
                    return Helper.GetPersianDate(CreationDate.Value);
                else
                    return null;
            }
        }
        public int Total { get; set; }
    }
}

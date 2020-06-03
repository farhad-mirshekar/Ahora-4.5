using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;

namespace FM.Portal.Core.Model
{
   public class FAQ : Entity
    {
        public Guid FAQGroupID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public Guid CreatorID { get; set; }

        //only show
        public string CreationDatePersian => Helper.GetPersianDate(CreationDate);

    }
}

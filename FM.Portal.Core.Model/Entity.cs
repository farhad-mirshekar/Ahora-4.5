using FM.Portal.Core.Common;
using System;
namespace FM.Portal.BaseModel
{
    public class Entity
    {
        public Guid ID { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationDatePersian
        {
            get
            {
                if (!CreationDate.HasValue)
                    return "";

                return Helper.GetPersianDate(CreationDate.Value);
            }
        }
    }
}

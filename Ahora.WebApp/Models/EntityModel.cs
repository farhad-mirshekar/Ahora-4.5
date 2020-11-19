using FM.Portal.Core.Common;
using System;

namespace Ahora.WebApp.Models
{
    public class EntityModel
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
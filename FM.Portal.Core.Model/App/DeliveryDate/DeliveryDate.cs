using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;

namespace FM.Portal.Core.Model
{
   public class DeliveryDate:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public EnableMenuType Enabled { get; set; }
        public Guid UserID { get; set; }
        public int Priority { get; set; }

        //only show
        public int Total { get; set; }
    }
}

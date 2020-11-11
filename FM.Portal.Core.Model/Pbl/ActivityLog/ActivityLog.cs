using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class ActivityLog:Entity
    {
        public Guid? UserID { get; set; }
        public string Comment { get; set; }
        public Guid ActivityLogTypeID { get; set; }
        public string IpAddress { get; set; }
        public Guid? EntityID { get; set; }
        public string EntityName { get; set; }
        //only show
        public int Total { get; set; }
        public string CreatorName { get; set; }
        public string SystemKeyword { get; set; }
    }
}

using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class Notification:Entity
    {
        public Guid UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReadDate { get; set; }
    }
}

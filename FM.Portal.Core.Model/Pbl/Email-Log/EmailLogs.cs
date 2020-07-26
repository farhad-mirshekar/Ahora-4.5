using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class EmailLogs:Entity
    {
        public Guid UserID { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public string IP { get; set; }
        public EmailStatusType EmailStatusType { get; set; }
    }
}

using System;

namespace FM.Portal.Core.Model
{
   public class PositionDefaultVM
    {
        public Guid UserID { get; set; }
        public Guid PositionID { get; set; }
        public string UserName { get; set; }
        public Guid ApplicationID { get; set; }
        public Guid DepartmentID { get; set; }
    }
}

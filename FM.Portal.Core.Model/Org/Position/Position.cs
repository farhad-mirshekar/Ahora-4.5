using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
    public class Position : Entity
    {
        public string Node { get; set; }
        public string ParentNode { get; set; }
        public Guid ApplicationID { get; set; }
        public Guid DepartmentID { get; set; }
        public PositionType Type { get; set; }
        public UserType UserType { get; set; }
        public Guid ParentID { get; set; }
        public Guid UserID { get; set; }
        public string Json { get; set; } //only for command but not exist in table role

        //only show
        public string UserInfo { get; set; }
    }
}

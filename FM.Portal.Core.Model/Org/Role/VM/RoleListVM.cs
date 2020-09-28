using System;

namespace FM.Portal.Core.Model
{
    public class RoleListVM : Pagination
    {
        public Guid? PositionID { get; set; }
        public Guid? UserID { get; set; }
    }
}

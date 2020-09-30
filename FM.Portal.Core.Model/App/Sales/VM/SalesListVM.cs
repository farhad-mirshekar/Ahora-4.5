using System;

namespace FM.Portal.Core.Model
{
    public class SalesListVM : Pagination
    {
        public Guid? ToPositionID { get; set; }
        public ActionState ActionState { get; set; }
    }
}

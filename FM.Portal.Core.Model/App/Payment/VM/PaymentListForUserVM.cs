using System;

namespace FM.Portal.Core.Model
{
   public class PaymentListForUserVM : Pagination
    {
        public Guid UserID { get; set; }
    }
}

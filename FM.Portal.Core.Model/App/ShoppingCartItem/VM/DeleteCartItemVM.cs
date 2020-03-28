using System;

namespace FM.Portal.Core.Model
{
   public class DeleteCartItemVM
    {
        public Guid ShoppingID { get; set; }
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
    }
}

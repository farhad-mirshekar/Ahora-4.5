using System;

namespace FM.Portal.Core.Model
{
   public class ProductListVM: Pagination
    {
        public Guid? CategoryID { get; set; }
        public bool? ShowOnHomePage { get; set; }
        public HasDiscountType HasDiscount { get; set; }
        public bool? SpecialOffer { get; set; }
    }
}

using System;

namespace FM.Portal.Core.Model
{
   public class ProductListVM
    {
        public Guid? CategoryID { get; set; }
        public bool? ShowOnHomePage { get; set; }
        public bool? HasDiscount { get; set; }
        public bool? SpecialOffer { get; set; }
    }
}

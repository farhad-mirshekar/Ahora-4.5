namespace FM.Portal.Core.Model
{
   public class DiscountListVM:Pagination
    {
        public string Name { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}

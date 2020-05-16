namespace FM.Portal.Core.Model
{
   public class ListProductShowOnHomePageVM
    {
        public string TrackingCode { get; set; }
        public string Name { get; set; }
        public PathType PathType { get; set; }
        public string FileName { get; set; }
        public bool HasDiscountsApplied { get; set; }
        public string DiscountName { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public int DiscountPercentage { get; set; }
        public DiscountType SelfProductDiscountType { get; set; }
        public decimal SelfProductDiscount { get; set; }
        public decimal Price { get; set; }
        public bool CallForPrice { get; set; }
    }
}

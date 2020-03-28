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
    }
}

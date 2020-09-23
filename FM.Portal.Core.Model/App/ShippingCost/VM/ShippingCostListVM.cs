namespace FM.Portal.Core.Model
{
    public class ShippingCostListVM : Pagination
    {
        public EnableMenuType Enabled { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; }
    }
}

namespace FM.Portal.Core.Model
{
   public class DeliveryDateListVM:Pagination
    {
        public EnableMenuType Enabled { get; set; }
        public int? Priority { get; set; }
        public string Name { get; set; }
    }
}

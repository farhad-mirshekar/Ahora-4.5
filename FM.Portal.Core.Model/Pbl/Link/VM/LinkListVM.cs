namespace FM.Portal.Core.Model
{
   public class LinkListVM:Pagination
    {
        public bool? ShowFooter { get; set; }
        public string Name { get; set; }
        public int? Priority { get; set; }
        public EnableMenuType Enabled { get; set; }
    }
}

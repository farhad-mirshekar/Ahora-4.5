using FM.Portal.BaseModel;

namespace FM.Portal.Core.Model
{
   public class Discount : Entity
    {
        public string Name { get; set; }
        public DiscountType DiscountType { get; set; }
        public int DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}

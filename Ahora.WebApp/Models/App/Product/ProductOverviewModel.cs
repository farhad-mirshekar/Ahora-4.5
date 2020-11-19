using FM.Portal.Core.Model;
using System;

namespace Ahora.WebApp.Models.App
{
    public class ProductOverviewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool CallForPrice { get; set; }
        public Decimal Price { get; set; }
        public HasDiscountType hasDiscount { get; set; }
        public Decimal Discount { get; set; }
        public DiscountType DiscountType { get; set; }
        public Guid CategoryID { get; set; }
        public Category Category { get; set; }
        public Guid DiscountID { get; set; }
        public Discount CategoryDiscount { get; set; }
        public Attachment PictureAttachment { get; set; }
        public string Path => "Product";
    }
}
using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
    public class ProductVariantAttributeValue : Entity
    {
        public Guid ProductVariantAttributeID { get; set; }
        public string Name { get; set; }
        public bool IsPreSelected { get; set; }
        public decimal Price { get; set; }
    }
}

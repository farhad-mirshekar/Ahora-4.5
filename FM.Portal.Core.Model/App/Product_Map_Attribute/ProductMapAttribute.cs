using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class ProductMapAttribute : Entity
    {
        public Guid ProductID { get; set; }
        public Guid ProductAttributeID { get; set; }
        public string TextPrompt { get; set; }
        public bool IsRequired { get; set; }
        public AttributeControlType AttributeControlType { get; set; }
    }
}

using FM.Portal.BaseModel;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class ListAttributeForSelectCustomerVM:Entity
    {
        public Guid ProductID { get; set; }
        public Guid AttributeID { get; set; }
        public string TextPrompt { get; set; }
        public AttributeControlType AttributeControlType { get; set; }
        public bool IsRequired { get; set; }
        public List<ListSubAttribute> ProductVariantAttributeValue { get; set; }
    }
    public class ListSubAttribute
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool IsPreSelected { get; set; }
        public decimal Price { get; set; }
    }
}

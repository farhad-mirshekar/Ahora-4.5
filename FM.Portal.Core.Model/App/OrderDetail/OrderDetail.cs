using FM.Portal.BaseModel;
using System;
namespace FM.Portal.Core.Model
{
   public class OrderDetail:Entity
    {
        public Guid OrderID { get; set; }
        public string ProductJson { get; set; }
        public string UserJson { get; set; }
        public string AttributeJson { get; set; }
    }
}

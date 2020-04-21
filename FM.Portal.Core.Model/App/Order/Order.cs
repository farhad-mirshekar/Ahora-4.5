using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class Order:Entity
    {
        public Guid ShoppingID { get; set; }
        public Guid UserID { get; set; }
        public SendType SendType { get; set; }
        public Guid AddressID { get; set; }
        public decimal Price { get; set; }
        public Guid BankID { get; set; }
        public string TrackingCode { get; set; }
    }
}

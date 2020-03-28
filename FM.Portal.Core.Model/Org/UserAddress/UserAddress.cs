using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class UserAddress : Entity
    {
        public Guid UserID { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string CellPhone { get; set; }
    }
}

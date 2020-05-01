using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class Download:Entity
    {
        public Guid UserID { get; set; }
        public Guid PaymentID { get; set; }
        public string Comment  { get; set; }
        public byte[] Data { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}

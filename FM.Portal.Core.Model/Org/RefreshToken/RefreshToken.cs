using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class RefreshToken:Entity
    {
        public Guid UserID { get; set; }

        public DateTime IssuedDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public string ProtectedTicket { get; set; }

        public bool Expired => DateTime.Now > ExpireDate;
    }
}

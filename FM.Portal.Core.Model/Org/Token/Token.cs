using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{ 
   public class Token : Entity
    {
        public Guid client_id { get; set; }

        public string client_secret { get; set; }

        public string grant_type { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        //public LoginType LoginType { get; set; }

        public string CellPhone { get; set; }

        public string Email { get; set; }

        public string SecurityStamp { get; set; }
        public string RefreshToken { get; set; }
    }
}

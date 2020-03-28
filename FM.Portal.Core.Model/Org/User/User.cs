

using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class User : Entity
    {

        public int Total { get; set; }

        public bool Enabled { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime? PasswordExpireDate { get; set; }

        public UserType Type { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NationalCode { get; set; }

        public string Email { get; set; }

        public bool EmailVerified { get; set; }
        public string CellPhone { get; set; }

        public bool CellPhoneVerified { get; set; }
    }
}

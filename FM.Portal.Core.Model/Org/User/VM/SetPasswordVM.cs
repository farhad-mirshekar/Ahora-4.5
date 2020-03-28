using System;

namespace FM.Portal.Core.Model
{
   public class SetPasswordVM
    {
        public Guid UserID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ReNewPassword { get; set; }
    }
}

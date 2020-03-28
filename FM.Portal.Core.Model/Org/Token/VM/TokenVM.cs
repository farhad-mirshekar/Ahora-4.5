

using System;

namespace FM.Portal.Core.Model
{
   public class TokenVM
    {
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
        public Guid UserID { get; set; }
        public string UserName { get; set; }

    }
}

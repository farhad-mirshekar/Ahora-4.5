using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Portal.Core.Owin
{
   public static class Extensions
    {
        public static string GetUserId(this System.Security.Principal.IIdentity identity)
        {
            var claimIdentity = identity as System.Security.Claims.ClaimsIdentity;
            var id = claimIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            return id == null ? string.Empty : id.Value;
        }

        public static T GetUserId<T>(this System.Security.Principal.IIdentity identity)
        {
            var claimIdentity = identity as System.Security.Claims.ClaimsIdentity;
            var id = claimIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            return id == null ? default(T) : (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(id.Value.ToString());
        }
    }
}

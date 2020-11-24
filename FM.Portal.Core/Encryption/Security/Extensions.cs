using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Portal.Core.Security
{
   public static class Extensions
    {
        public static string HashText(this string plainText)
             => Encryption.HashHelper.Instance.GetHash($"!<{plainText}]?");
    }
}

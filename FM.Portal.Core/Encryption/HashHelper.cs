using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FM.Portal.Core.Encryption
{
    public class HashHelper
    {
        private static readonly Lazy<HashHelper> lazy = new Lazy<HashHelper>(() => new HashHelper());

        public static HashHelper Instance => lazy.Value;

        private HashHelper() { }

        public string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}

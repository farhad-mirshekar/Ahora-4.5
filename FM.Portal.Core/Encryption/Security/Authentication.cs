using System.Text;

namespace FM.Portal.Core.Encryption.Security
{
   public static class Authentication
    {
        public static string EncryptDES(string plainText)
        {
            cTripleDES cT = new cTripleDES(Encoding.ASCII.GetBytes("amour_1373_fatem"), Encoding.ASCII.GetBytes("123456789_526372"));
            string str = cT.Encrypt(plainText);
            return str;
        }

        public static string DecryptDES(string plainText)
        {
            cTripleDES cT = new cTripleDES(Encoding.ASCII.GetBytes("amour_1373_fatem"), Encoding.ASCII.GetBytes("123456789_526372"));
            string str = cT.Decrypt(plainText);
            return str;
        }
    }
}

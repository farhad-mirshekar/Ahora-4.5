using System.Text;

namespace FM.Portal.Core.Encryption
{
   public static class Authentication
    {
        public static string EncryptDES(string plainText)
        {
            var cT = new cTripleDES(Encoding.ASCII.GetBytes("armin_1366_etteh"), Encoding.ASCII.GetBytes("123456789_526372"));
            string str = cT.Encrypt(plainText);
            return str;
        }

        public static string DecryptDES(string plainText)
        {
            var cT = new cTripleDES(Encoding.ASCII.GetBytes("armin_1366_etteh"), Encoding.ASCII.GetBytes("123456789_526372"));
            string str = cT.Decrypt(plainText);
            return str;
        }
    }
}

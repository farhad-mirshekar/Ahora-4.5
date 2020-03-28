namespace FM.Portal.Core.Model
{ 
   public class LoginVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NationalCode { get; set; }
        public string RefreshToken { get; set; }
    }
}

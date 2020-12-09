using FM.Portal.FrameWork.Attributes;

namespace Ahora.WebApp.Models.Org
{
    public class LoginModel
    {
        [Required("Account.Login.Field.Username.ErrorMessage")]
        [DisplayName("Account.Login.Field.Username")]
        public string Username { get; set; }
        [Required("Account.Login.Field.Password.ErrorMessage")]
        [DisplayName("Account.Login.Field.Password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool ShowCaptcha { get; set; }
        public string Captcha { get; set; }
        public string CaptchaText { get; set; }
        public string ReturnUrl { get; set; }

        //only js
        public string authorizationData { get; set; }
        public string currentUserPositions { get; set; }
        public string currentUserPosition { get; set; }
        public int userType { get; set; }
        public bool status { get; set; }
        public string ErrorText { get; set; }
    }
}
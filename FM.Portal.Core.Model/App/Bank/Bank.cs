using FM.Portal.BaseModel;

namespace FM.Portal.Core.Model
{
   public class Bank : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MyProperty { get; set; }
        public BankName BankName { get; set; }
        public string MerchantID { get; set; }
        public string MerchantKey { get; set; }
        public string TerminalID { get; set; }
        public string Url { get; set; }
        public string RedirectUrl { get; set; }
        public bool Default { get; set; }
    }
}

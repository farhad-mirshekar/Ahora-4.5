using Attribute =  FM.Portal.FrameWork.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace Ahora.WebApp.Models.Org
{
    public class RegisterModel : EntityModel
    {
        [Attribute.Required("Account.Register.Field.Firstname.ErrorMessage")]
        public string FirstName { get; set; }
        [Attribute.Required("Account.Register.Field.LastName.ErrorMessage")]
        public string LastName { get; set; }
        [Attribute.Required("Account.Register.Field.NationalCode.ErrorMessage")]
        public string NationalCode { get; set; }
        [Attribute.Required("Account.Register.Field.CellPhone.ErrorMessage")]
        public string CellPhone { get; set; }
        [Attribute.Required("Account.Register.Field.Password.ErrorMessage"), MinLength(8, ErrorMessage = "رمز عبور نمی تواند کمتر از 8 کاراکتر باشد")]
        public string Password { get; set; }
        [Attribute.Required("Account.Register.Field.RePassword.ErrorMessage"), MinLength(8, ErrorMessage = "رمز عبور نمی تواند کمتر از 8 کاراکتر باشد")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار رمز عبور باید یکسان باشند")]
        public string RePassword { get; set; }
        [Attribute.Required("Account.Register.Field.UserName.ErrorMessage")]
        [MinLength(8, ErrorMessage = "نام کاربری نمی تواند کمتر از 8 کاراکتر باشد")]
        [Remote("IsAlreadyUserName", "Account", HttpMethod = "POST", ErrorMessage = "نام کاربری تکراری می باشد")]
        public string UserName { get; set; }
    }
}
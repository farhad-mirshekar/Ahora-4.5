using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace Ahora.WebApp.Models.Org
{
    public class RegisterModel : EntityModel
    {
        [Required(ErrorMessage = "لطفا نام خود را وارد نمایید")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "لطفا نام خانوادگی خود را وارد نمایید")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "لطفا کد ملی خود را وارد نمایید")]
        public string NationalCode { get; set; }
        [Required(ErrorMessage = "لطفا شماره همراه خود را وارد نمایید")]
        public string CellPhone { get; set; }
        [Required(ErrorMessage = "لطفا رمز عبور خود را وارد نمایید"), MinLength(8, ErrorMessage = "رمز عبور نمی تواند کمتر از 8 کاراکتر باشد")]
        public string Password { get; set; }
        [Required(ErrorMessage = "لطفا تکرار رمز عبور خود را وارد نمایید"), MinLength(8, ErrorMessage = "رمز عبور نمی تواند کمتر از 8 کاراکتر باشد")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار رمز عبور باید یکسان باشند")]
        public string RePassword { get; set; }
        [Required(ErrorMessage = "نام کاربری خود را وارد نمایید")]
        [MinLength(8, ErrorMessage = "نام کاربری نمی تواند کمتر از 8 کاراکتر باشد")]
        [Remote("IsAlreadyUserName", "Account", HttpMethod = "POST", ErrorMessage = "نام کاربری تکراری می باشد")]
        public string UserName { get; set; }
    }
}
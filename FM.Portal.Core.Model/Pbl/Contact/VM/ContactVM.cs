using System.ComponentModel.DataAnnotations;

namespace FM.Portal.Core.Model
{
   public class ContactVM
    {
        [Required(ErrorMessage = "لطفا نام خود را وارد نمایید")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "لطفا نام خانوادگی خود را وارد نمایید")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "لطفا شماره همراه خود را وارد نمایید")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "لطفا پست الکترونیکی خود را وارد نمایید")]
        public string Email { get; set; }
        [Required(ErrorMessage = "لطفا موضوع را وارد نمایید")]
        public string Title { get; set; }
        [Required(ErrorMessage = "لطفا متن پیام را وارد نمایید")]
        public string Description { get; set; }
    }
}

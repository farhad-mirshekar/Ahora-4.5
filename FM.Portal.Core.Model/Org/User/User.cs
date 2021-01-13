using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
    /// <summary>
    /// کاربر
    /// </summary>
   public class User : Entity
    {

        public int Total { get; set; }
        /// <summary>
        /// فعال بودن
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// نام کاربری
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// رمز عبور
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// زمان انقضا کاربر
        /// </summary>
        public DateTime? PasswordExpireDate { get; set; }
        /// <summary>
        /// نوع کاربر : درون سازمان یا بیرون سازمان
        /// </summary>

        public UserType Type { get; set; }
        /// <summary>
        /// نام 
        /// </summary>

        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// شماره ملی
        /// </summary>

        public string NationalCode { get; set; }
        /// <summary>
        /// پست الکترونیکی
        /// </summary>

        public string Email { get; set; }
        /// <summary>
        /// مجاز بودن ایمیل
        /// </summary>

        public bool EmailVerified { get; set; }
        /// <summary>
        /// شماره همراه
        /// </summary>
        public string CellPhone { get; set; }
        /// <summary>
        /// مجاز بودن شماره همراه
        /// </summary>

        public bool CellPhoneVerified { get; set; }
    }
}

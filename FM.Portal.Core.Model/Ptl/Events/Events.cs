using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
    /// <summary>
    /// رویداد
    /// </summary>
    public class Events : Entity
    {
        public Events()
        {
            Tags = new List<string>();
        }
        /// <summary>
        /// عنوان رویداد
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// تاریخ آخرین ویرایش
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// متن اصلی
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// متا 
        /// </summary>
        public string MetaKeywords { get; set; }
        /// <summary>
        /// توضیحات کوتاه رویداد
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// وضعیت نظرات رویداد : باز یا بسته
        /// </summary>
        public CommentStatusType CommentStatusType { get; set; }
        /// <summary>
        /// تعداد بازدید رویداد
        /// </summary>
        public int VisitedCount { get; set; }
        /// <summary>
        /// تعداد موافق رویداد
        /// </summary>
        public int LikeCount { get; set; }
        /// <summary>
        /// تعداد مخالف رویداد
        /// </summary>
        public int DisLikeCount { get; set; }
        /// <summary>
        /// توضیحات برای سئو
        /// </summary>
        public string UrlDesc { get; set; }
        /// <summary>
        /// نحوه نمایش رویداد : نمایش یا عدم نمایش
        /// </summary>
        public ViewStatusType ViewStatusType { get; set; }
        /// <summary>
        /// آی دی دسته بندی
        /// </summary>
        public Guid CategoryID { get; set; }
        /// <summary>
        /// ایجاد کننده رویداد
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// چه کسی حذف کرده
        /// </summary>
        public Guid? RemoverID { get; set; }
        /// <summary>
        /// لیست تگ ها
        /// </summary>
        public List<String> Tags { get; set; }
        /// <summary>
        /// مدت زمان خواندن رویداد
        /// </summary>
        public string ReadingTime { get; set; }
        /// <summary>
        /// زبان رویداد
        /// </summary>
        public Guid? LanguageID { get; set; }
        /// <summary>
        /// زمان حذف رویداد
        /// </summary>
        public DateTime? RemoverDate { get; set; }

        //for only show 
        /// <summary>
        /// نام ایجاد کننده رویداد
        /// </summary>
        public string CreatorName { get; set; }
        /// <summary>
        /// مسیر عکس
        /// </summary>
        public string Path => "Events";
        public int Total { get; set; }
    }
}

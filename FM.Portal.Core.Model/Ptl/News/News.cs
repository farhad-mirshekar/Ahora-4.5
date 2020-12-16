using FM.Portal.BaseModel;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
    /// <summary>
    /// خبر
    /// </summary>
    public class News : Entity
    {
        public News()
        {
            Tags = new List<string>();
        }
        /// <summary>
        /// عنوان خبر
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
        /// توضیحات کوتاه خبر
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// وضعیت نظرات خبر : باز یا بسته
        /// </summary>
        public CommentStatusType CommentStatusType { get; set; }
        /// <summary>
        /// تعداد بازدید خبر
        /// </summary>
        public int VisitedCount { get; set; }
        /// <summary>
        /// تعداد موافق خبر
        /// </summary>
        public int LikeCount { get; set; }
        /// <summary>
        /// تعداد مخالف خبر
        /// </summary>
        public int DisLikeCount { get; set; }
        /// <summary>
        /// توضیحات برای سئو
        /// </summary>
        public string UrlDesc { get; set; }
        /// <summary>
        /// نحوه نمایش خبر : نمایش یا عدم نمایش
        /// </summary>
        public ViewStatusType ViewStatusType { get; set; }
        /// <summary>
        /// آی دی دسته بندی
        /// </summary>
        public Guid CategoryID { get; set; }
        /// <summary>
        /// ایجاد کننده خبر
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
        /// مدت زمان خواندن خبر
        /// </summary>
        public string ReadingTime { get; set; }
        /// <summary>
        /// زبان خبر
        /// </summary>
        public Guid? LanguageID { get; set; }
        /// <summary>
        /// زمان حذف خبر
        /// </summary>
        public DateTime? RemoverDate { get; set; }

        //for only show 
        /// <summary>
        /// نام ایجاد کننده خبر
        /// </summary>
        public string CreatorName { get; set; }
        /// <summary>
        /// مسیر عکس
        /// </summary>
        public string Path => "News";
        public int Total { get; set; }
    }
}


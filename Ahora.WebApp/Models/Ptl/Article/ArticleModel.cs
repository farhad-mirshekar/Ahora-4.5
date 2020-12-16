using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace Ahora.WebApp.Models.Ptl
{
    public class ArticleModel : EntityModel
    {
        public ArticleModel()
        {
            Tags = new List<string>();
        }
        /// <summary>
        /// عنوان مقاله
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
        /// توضیحات کوتاه مقاله
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// وضعیت نظرات مقاله : باز یا بسته
        /// </summary>
        public CommentStatusType CommentStatusType { get; set; }
        /// <summary>
        /// تعداد بازدید مقاله
        /// </summary>
        public int VisitedCount { get; set; }
        /// <summary>
        /// تعداد موافق مقاله
        /// </summary>
        public int LikeCount { get; set; }
        /// <summary>
        /// تعداد مخالف مقاله
        /// </summary>
        public int DisLikeCount { get; set; }
        /// <summary>
        /// توضیحات برای سئو
        /// </summary>
        public string UrlDesc { get; set; }
        /// <summary>
        /// نحوه نمایش مقاله : نمایش یا عدم نمایش
        /// </summary>
        public ViewStatusType ViewStatusType { get; set; }
        /// <summary>
        /// آی دی دسته بندی
        /// </summary>
        public Guid CategoryID { get; set; }
        /// <summary>
        /// ایجاد کننده مقاله
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
        /// مدت زمان خواندن مقاله
        /// </summary>
        public string ReadingTime { get; set; }
        /// <summary>
        /// زبان مقاله
        /// </summary>
        public Guid? LanguageID { get; set; }
        /// <summary>
        /// زمان حذف مقاله
        /// </summary>
        public DateTime? RemoverDate { get; set; }

        //for only show 
        /// <summary>
        /// نام ایجاد کننده مقاله
        /// </summary>
        public string CreatorName { get; set; }
        /// <summary>
        /// مسیر عکس
        /// </summary>
        public string Path => "Article";
        public int Total { get; set; }
        public List<Attachment> PictureAttachments { get; set; }
        public List<Attachment> VideoAttachments { get; set; }
    }
}
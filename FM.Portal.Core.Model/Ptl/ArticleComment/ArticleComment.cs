using FM.Portal.BaseModel;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
    /// <summary>
    /// نظرات مقالات
    /// </summary>
   public class ArticleComment:Entity
    {
        /// <summary>
        /// متن اصلی
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// نمایش مقاله : درحال بررسی - تایید - عدم تایید
        /// </summary>
        public CommentType CommentType { get; set; }
        /// <summary>
        /// تعداد لایک
        /// </summary>
        public int LikeCount { get; set; }
        /// <summary>
        /// تعداد ناپسند
        /// </summary>
        public int DisLikeCount { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// آی دی مقاله
        /// </summary>
        public Guid ArticleID { get; set; }
        public Guid RemoverID { get; set; }
        public Guid? ParentID { get; set; }

        //only show
        public string CreatorName { get; set; }
        public List<ArticleComment> Children { get; set; }
        public int Total { get; set; }
        public string ArticleTitle { get; set; }
    }
}

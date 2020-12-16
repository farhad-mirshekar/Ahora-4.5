using FM.Portal.BaseModel;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class NewsComment:Entity
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
        /// آی دی خبر
        /// </summary>
        public Guid NewsID { get; set; }
        public Guid? RemoverID { get; set; }
        public Guid? ParentID { get; set; }

        //only show
        public string CreatorName { get; set; }
        public List<NewsComment> Children { get; set; }
        public int Total { get; set; }
        public string NewsTitle { get; set; }
    }
}

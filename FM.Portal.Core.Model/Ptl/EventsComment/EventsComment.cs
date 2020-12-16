using FM.Portal.BaseModel;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
    /// <summary>
    /// نظرات رویداد
    /// </summary>
   public class EventsComment:Entity
    {
        /// <summary>
        /// متن نظر
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
        /// آی دی رویداد
        /// </summary>
        public Guid EventsID { get; set; }
        public Guid? RemoverID { get; set; }
        public Guid? ParentID { get; set; }

        //only show
        public string CreatorName { get; set; }
        public List<EventsComment> Children { get; set; }
        public int Total { get; set; }
        public string EventsTitle { get; set; }
    }
}

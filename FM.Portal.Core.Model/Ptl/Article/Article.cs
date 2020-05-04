using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class Article : Entity
    {
        public string Title { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Body { get; set; }
        public string MetaKeywords { get; set; }
        public string Description { get; set; }
        public CommentArticleType CommentStatus { get; set; }
        public int VisitedCount { get; set; }
        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }
        public string UrlDesc { get; set; }
        public bool IsShow { get; set; }
        public Guid CategoryID { get; set; }
        public Guid UserID { get; set; }
        public Guid RemoverID { get; set; }
        public string TrackingCode { get; set; }
        public List<String> Tags { get; set; }
        public string ReadingTime { get; set; }

        //for only show 
        public string CreatorName { get; set; }
        public string FileName { get; set; }
        public PathType PathType { get; set; }

        public string Path => PathType.ToString();
        public string CreationDatePersian => Helper.GetPersianDate(CreationDate);
    }
}

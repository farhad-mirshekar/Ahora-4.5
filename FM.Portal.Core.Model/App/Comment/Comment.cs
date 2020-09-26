using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class Comment:Entity
    {
        public string Body { get; set; }
        public CommentType CommentType { get; set; }
        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }
        public Guid UserID { get; set; }
        public Guid DocumentID { get; set; }
        public Guid RemoverID { get; set; }
        public Guid ParentID { get; set; }
        public CommentForType CommentForType { get; set; }

        //only show
        public string CreatorName { get; set; }
        public List<Comment> Children { get; set; }
        public string CreationDatePersian => Helper.GetPersianDate(CreationDate);
        public int Total { get; set; }
        public string ProductName { get; set; }
    }
}

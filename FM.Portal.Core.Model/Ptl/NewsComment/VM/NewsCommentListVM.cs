using System;

namespace FM.Portal.Core.Model
{
   public class NewsCommentListVM:Pagination
    {
        public Guid? NewsID { get; set; }
        public Guid? UserID { get; set; }
        public Guid? ParentID { get; set; }
        public CommentType CommentType { get; set; }
        public bool ShowChildren { get; set; }
    }
}

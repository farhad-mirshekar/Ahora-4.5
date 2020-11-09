using System;

namespace FM.Portal.Core.Model
{
   public class ProductCommentListVM:Pagination
    {
        public Guid? ProductID { get; set; }
        public Guid? UserID { get; set; }
        public Guid? ParentID { get; set; }
        public CommentType CommentType { get; set; }
        public bool ShowChildren { get; set; }
    }
}

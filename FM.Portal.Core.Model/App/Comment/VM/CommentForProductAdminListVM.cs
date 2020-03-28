using FM.Portal.BaseModel;
using System;
namespace FM.Portal.Core.Model
{
   public class CommentForProductAdminListVM:Entity
    {
        public string Body { get; set; }
        public CommentType CommentType { get; set; }
        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }
        public Guid UserID { get; set; }
        public Guid DocumentID { get; set; }
        public Guid RemoverID { get; set; }
        public Guid ParentID { get; set; }
        public string NameFamily { get; set; }
        public string ProductName { get; set; }
    }
}

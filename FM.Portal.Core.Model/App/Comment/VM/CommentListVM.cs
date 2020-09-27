using System;

namespace FM.Portal.Core.Model
{
   public class CommentListVM:Pagination
    {
        public CommentListVM()
        {
            ShowChildren = true;
            OnlyProduct = true;
        }
        public CommentType CommentType { get; set; }
        public CommentForType CommentForType { get; set; }
        public Guid? ParentID { get; set; }
        public Guid? DocumentID { get; set; }
        public bool ShowChildren { get; set; }
        public bool OnlyProduct { get; set; }
    }
}

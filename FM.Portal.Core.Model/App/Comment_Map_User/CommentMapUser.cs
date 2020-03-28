using System;

namespace FM.Portal.Core.Model
{
   public class CommentMapUser
    {
        public Guid CommentID { get; set; }
        public Guid UserID { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

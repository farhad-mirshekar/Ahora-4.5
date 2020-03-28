
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class CommentListVM
    {
        public Guid DocumentID { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}

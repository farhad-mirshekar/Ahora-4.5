﻿using FM.Portal.BaseModel;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class ProductComment:Entity
    {
        public string Body { get; set; }
        public CommentType CommentType { get; set; }
        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
        public Guid? RemoverID { get; set; }
        public Guid? ParentID { get; set; }

        //only show
        public string CreatorName { get; set; }
        public List<ProductComment> Children { get; set; }
        public int Total { get; set; }
    }
}

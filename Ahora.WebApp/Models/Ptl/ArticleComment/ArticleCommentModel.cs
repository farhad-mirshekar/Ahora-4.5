﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Ahora.WebApp.Models.Ptl
{
    public class ArticleCommentModel
    {
        [Required(ErrorMessage ="متن نظر را وارد نمایید")]
        public string Body { get; set; }
        public Guid ID { get; set; }
        public Guid? ParentID { get; set; }
        public Guid UserID { get; set; }
        public Guid ArticleID { get; set; }
    }
}
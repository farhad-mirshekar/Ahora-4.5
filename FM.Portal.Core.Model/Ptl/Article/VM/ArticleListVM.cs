﻿using System;

namespace FM.Portal.Core.Model
{
   public class ArticleListVM:Pagination
    {
        public string Title { get; set; }
        public Guid? LanguageID { get; set; }
    }
}

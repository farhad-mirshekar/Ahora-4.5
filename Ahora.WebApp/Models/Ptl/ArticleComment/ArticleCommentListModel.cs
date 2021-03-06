﻿using @Model= FM.Portal.Core.Model;
using System.Collections.Generic;
using FM.Portal.Core.Model;

namespace Ahora.WebApp.Models.Ptl
{
    public class ArticleCommentListModel
    {
        public ArticleCommentListModel()
        {
            AvailableComments = new List<ArticleComment>();
            Article = new Article();
        }
        public List<Model.ArticleComment> AvailableComments { get; set; }
        public User User { get; set; }
        public Model.Article Article { get; set; }
    }
}
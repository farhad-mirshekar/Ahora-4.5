using @Model= FM.Portal.Core.Model;
using System.Collections.Generic;
using FM.Portal.Core.Model;

namespace Ahora.WebApp.Models.Ptl.ArticleComment
{
    public class ArticleCommentListModel
    {
        public List<Model.ArticleComment> AvailableComments { get; set; }
        public User User { get; set; }
        public Model.Article Article { get; set; }
    }
}
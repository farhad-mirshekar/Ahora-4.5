using @Model = FM.Portal.Core.Model;
using System.Collections.Generic;

namespace Ahora.WebApp.Models.Ptl.Article
{
    public class ArticleListModel
    {
        public List<Model.Article> AvailableArticles { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
using System.Collections.Generic;

namespace Ahora.WebApp.Models.Ptl
{
    public class ArticleListModel
    {
        public ArticleListModel()
        {
            AvailableArticles = new List<ArticleModel>();
        }
        public List<ArticleModel> AvailableArticles { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
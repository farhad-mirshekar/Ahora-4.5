using @Model= FM.Portal.Core.Model;
using System.Collections.Generic;
using FM.Portal.Core.Model;

namespace Ahora.WebApp.Models.Ptl
{
    public class NewsCommentListModel
    {
        public NewsCommentListModel()
        {
            AvailableComments = new List<NewsComment>();
            News = new News();
        }
        public List<Model.NewsComment> AvailableComments { get; set; }
        public User User { get; set; }
        public Model.News News { get; set; }
    }
}
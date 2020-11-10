using @Model= FM.Portal.Core.Model;
using System.Collections.Generic;
using FM.Portal.Core.Model;

namespace Ahora.WebApp.Models.Ptl
{
    public class NewsCommentListModel
    {
        public List<Model.NewsComment> AvailableComments { get; set; }
        public User User { get; set; }
        public Model.News News { get; set; }
    }
}
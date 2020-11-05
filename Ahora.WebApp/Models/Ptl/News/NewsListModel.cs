using @Model = FM.Portal.Core.Model;
using System.Collections.Generic;

namespace Ahora.WebApp.Models.Ptl.News
{
    public class ArticleListModel
    {
        public List<Model.News> AvailableNews { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
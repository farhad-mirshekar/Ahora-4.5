using System.Collections.Generic;

namespace Ahora.WebApp.Models.Ptl
{
    public class NewsListModel
    {
        public NewsListModel()
        {
            AvailableNews = new List<NewsModel>();
        }
        public List<NewsModel> AvailableNews { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
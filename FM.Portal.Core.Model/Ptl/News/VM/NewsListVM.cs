using FM.Portal.BaseModel;

namespace FM.Portal.Core.Model
{
   public class NewsListVM : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlDesc { get; set; }
        public string TrackingCode { get; set; }
        public string CreatorName { get; set; }
        public PathType PathType { get; set; }
        public string Path => PathType.ToString();
        public string FileName { get; set; }
    }
}

using FM.Portal.BaseModel;

namespace FM.Portal.Core.Model
{
   public class Slider : Entity
    {
        public string Title { get; set; }
        public int Priority { get; set; }
        public EnableMenuType Enabled { get; set; }
        public bool Deleted { get; set; }
        public string Url { get; set; }

        //only show
        public string FileName { get; set; }
        public PathType PathType { get; set; }
        public string Path => PathType.ToString();
    }
}

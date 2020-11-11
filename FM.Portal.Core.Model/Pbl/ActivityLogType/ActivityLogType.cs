using FM.Portal.BaseModel;

namespace FM.Portal.Core.Model
{
   public class ActivityLogType:Entity
    {
        public string SystemKeyword { get; set; }
        public string Name { get; set; }
        public EnableMenuType Enabled { get; set; }
        //only show
        public int Total { get; set; }
    }
}

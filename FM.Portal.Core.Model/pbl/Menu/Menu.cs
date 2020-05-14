using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class Menu : Entity
    {
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public EnableMenuType Enabled { get; set; }
        public string Node { get; set; }
        public string ParentNode { get; set; }
        public Guid ParentID { get; set; }
        public string Url { get; set; }
        public string IconText { get; set; }
        public int Priority { get; set; }
        public string Parameters { get; set; }
    }
}

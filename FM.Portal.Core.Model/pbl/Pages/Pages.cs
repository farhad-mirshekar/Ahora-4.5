using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class Pages : Entity
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Deleted { get; set; }
        public bool Enabled { get; set; }
        public string Node { get; set; }
        public string ParentNode { get; set; }
        public Guid ParentID { get; set; }
        public string RouteUrl { get; set; }
    }
}

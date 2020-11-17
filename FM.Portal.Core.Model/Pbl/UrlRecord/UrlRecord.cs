using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class UrlRecord:Entity
    {
        public Guid EntityID { get; set; }
        public string EntityName { get; set; }
        public string UrlDesc { get; set; }
        public EnableMenuType Enabled { get; set; }
    }
}

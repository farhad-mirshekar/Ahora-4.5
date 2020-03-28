
using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class FAQGroup : Entity
    {
        public string Title { get; set; }
        public Guid CreatorID { get; set; }
        public Guid ApplicationID { get; set; }
    }
}

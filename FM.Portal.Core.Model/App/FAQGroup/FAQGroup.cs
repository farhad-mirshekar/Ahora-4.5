
using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;

namespace FM.Portal.Core.Model
{
   public class FAQGroup : Entity
    {
        public string Title { get; set; }
        public Guid CreatorID { get; set; }
        public Guid ApplicationID { get; set; }

        //only show
        public int Total { get; set; }
        public int CountFaq { get; set; }
    }
}

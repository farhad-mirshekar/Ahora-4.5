using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class Tags:Entity
    {
        public Guid DocumentID { get; set; }
        public string Name { get; set; }
    }
}

using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class RelatedProduct:Entity
    {
        public Guid ProductID1 { get; set; }
        public Guid ProductID2 { get; set; }
        public int Priority { get; set; }

        //only show
        public string ProductName1 { get; set; }
        public string ProductName2 { get; set; }
    }
}

﻿using FM.Portal.BaseModel;
using FM.Portal.Core.Common;

namespace FM.Portal.Core.Model
{
   public class ProductAttribute : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //only show
        public int Total { get; set; }
    }
}

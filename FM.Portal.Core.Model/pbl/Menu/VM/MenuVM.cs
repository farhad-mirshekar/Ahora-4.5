using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class MenuVM
    {
        public Guid ID { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string IconText { get; set; }
        public List<MenuVM> Children { get; set; }
    }
}

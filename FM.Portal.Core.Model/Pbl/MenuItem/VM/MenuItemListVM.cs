using System;

namespace FM.Portal.Core.Model
{
   public class MenuItemListVM
    {
        public Guid? MenuID { get; set; }
        public string ParentNode { get; set; }
        public Guid? LanguageID { get; set; }
    }
}

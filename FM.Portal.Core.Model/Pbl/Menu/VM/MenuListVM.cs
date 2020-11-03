using System;

namespace FM.Portal.Core.Model
{
    public class MenuListVM : Pagination
    {
        public Guid? LanguageID { get; set; }
        public string Name { get; set; }
    }
}

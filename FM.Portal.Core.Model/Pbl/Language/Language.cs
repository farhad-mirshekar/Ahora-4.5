using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class Language:Entity
    {
        public string Name { get; set; }
        public LanguageCultureType LanguageCultureType { get; set; }
        public string UniqueSeoCode { get; set; }
        public EnableMenuType Enabled { get; set; }
        public Guid UserID { get; set; }

        //only show
        public int Total { get; set; }
        public Language CurrentLanguage { get; set; }
    }
}

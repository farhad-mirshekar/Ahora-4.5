using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;

namespace FM.Portal.Core.Model
{
   public class Menu : Entity
    {
        public string Name { get; set; }
        public Guid LanguageID { get; set; }
        public Guid RemoverID { get; set; }
        public DateTime RemoverDate { get; set; }
        public Guid UserID { get; set; }

        //only show
        public string LanguageName { get; set; }
        public int Total { get; set; }
    }
}

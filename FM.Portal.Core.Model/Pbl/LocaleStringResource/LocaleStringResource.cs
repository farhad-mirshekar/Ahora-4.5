using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
    public class LocaleStringResource : Entity
    {
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
        public Guid LanguageID { get; set; }
        public Guid UserID { get; set; }

        //only show
        public int Total { get; set; }
    }
}

using System;

namespace FM.Portal.Core.Model
{
   public class NewsListVM : Pagination
    {
        public string Title { get; set; }
        public Guid? LanguageID { get; set; }
        public ViewStatusType ViewStatusType { get; set; }
    }
}

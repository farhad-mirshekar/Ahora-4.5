using System;

namespace FM.Portal.Core.Model
{
   public class TagSearchVM
    {
        public Guid DocumentID { get; set; }
        public string EntityName { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentUrlDesc { get; set; }
        public int Total { get; set; }
        /// <summary>
        /// برای صفحات داینامیک یا استاتیک
        /// </summary>
        public string PageName { get; set; }
        public string DocumentDescription { get; set; }
    }
}

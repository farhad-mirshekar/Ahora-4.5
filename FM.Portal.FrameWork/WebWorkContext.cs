using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using System;

namespace FM.Portal.FrameWork
{
    public class WebWorkContext : IWorkContext
    {
        private readonly ILanguageService _languageService;
        public WebWorkContext(ILanguageService languageService)
        {
            _languageService = languageService;
        }
        public Language WorkingLanguage { get; set; }
    }
}

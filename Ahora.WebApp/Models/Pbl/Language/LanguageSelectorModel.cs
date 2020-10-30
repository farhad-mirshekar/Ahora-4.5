using System;
using System.Collections.Generic;

namespace Ahora.WebApp.Models.Pbl.Language
{
    public class LanguageSelectorModel
    {
        public LanguageSelectorModel()
        {
            AvailableLanguage = new List<LanguageModel>();
        }
        public List<LanguageModel> AvailableLanguage { get; set; }
        public Guid CurrentLanguageID { get; set; }
    }
}
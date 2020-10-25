using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
    public interface ILanguageService : IService
    {
        Result<Language> Add(Language model);
        Result<Language> Edit(Language model);
        Result<Language> Get(Guid ID);
        Result<List<Language>> List(LanguageListVM listVM);
        Result.Result Delete(Guid ID);
    }
}

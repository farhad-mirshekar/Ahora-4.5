using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
    public interface ILocaleStringResourceService : IService
    {
        Result<LocaleStringResource> Add(LocaleStringResource model);
        Result<LocaleStringResource> Edit(LocaleStringResource model);
        Result<LocaleStringResource> Get(Guid ID);
        Result<List<LocaleStringResource>> List(LocaleStringResourceListVM listVM);
        Result.Result Delete(Guid ID);
        Result<string> GetResource(string format);

    }
}

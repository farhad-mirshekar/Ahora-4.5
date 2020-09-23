using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IStaticPageService:IService
    {
        Result<StaticPage> Edit(StaticPage model);
        Result<StaticPage> Get(Guid ID);
        Result<StaticPage> Get(string TrackingCode);
        Result<List<StaticPage>> List(StaticPageListVM listVM);
        Result.Result Delete(Guid ID);
    }
}

using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IStaticPageService:IService
    {
        Result<StaticPage> Edit(StaticPage model);
        Result<StaticPage> Get(Guid ID);
        Result<List<StaticPage>> List(StaticPageListVM listVM);
        Result Delete(Guid ID);
    }
}

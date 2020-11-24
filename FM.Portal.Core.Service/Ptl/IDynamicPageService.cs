using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IDynamicPageService:IService
    {
        Result<DynamicPage> Add(DynamicPage model);
        Result<DynamicPage> Edit(DynamicPage model);
        Result<DynamicPage> Get(Guid ID);
        Result<List<DynamicPage>> List(DynamicPageListVM listVM);
        Result Delete(Guid ID);
    }
}

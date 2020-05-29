using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface ILinkService : IService
    {
        Result<Link> Add(Link model);
        Result<Link> Edit(Link model);
        Result<Link> Get(Guid ID);
        Result<List<Link>> List(LinkListVM listVM);
        Result.Result Delete(Guid ID);
    }
}

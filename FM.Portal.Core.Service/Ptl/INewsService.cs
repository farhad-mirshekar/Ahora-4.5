using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface INewsService:IService
    {
        Result<News> Add(News model);
        Result<News> Edit(News model);
        Result<List<News>> List(NewsListVM listVM);
        Result<News> Get(Guid ID);
        Result<int> Delete(Guid ID);

    }
}

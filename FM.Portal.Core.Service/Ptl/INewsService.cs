using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface INewsService:IService
    {
        Result<News> Add(News model);
        Result<News> Edit(News model);
        Result<List<News>> List();
        Result<List<NewsListVM>> List(int count);
        Result<News> Get(Guid ID);
        Result<News> Get(string TrackingCode);
        Result<int> Delete(Guid ID);

    }
}

using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IArticleService : IService
    {
        Result<Article> Add(Article model);
        Result<Article> Edit(Article model);
        Result<List<Article>> List();
        Result<List<ArticleListVM>> List(int count);
        Result<Article> Get(Guid ID);
        Result<Article> Get(string TrackingCode);
        Result<int> Delete(Guid ID);
    }
}

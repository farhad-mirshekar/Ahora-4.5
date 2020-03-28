using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
    public interface IArticleDataSource : IDataSource
    {
        Result<Article> Insert(Article model);
        Result<Article> Update(Article model);
        DataTable List();
        DataTable List(int count);
        Result<Article> Get(Guid ID);
        Result<Article> Get(string TrackingCode);
        Result<int> Delete(Guid ID);
    }
}

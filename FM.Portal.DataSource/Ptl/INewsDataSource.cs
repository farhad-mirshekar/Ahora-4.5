using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
    public interface INewsDataSource : IDataSource
    {
        Result<News> Insert(News model);
        Result<News> Update(News model);
        DataTable List();
        DataTable List(int count);
        Result<News> Get(Guid ID);
        Result<News> Get(string TrackingCode);
        Result<int> Delete(Guid ID);

    }
}

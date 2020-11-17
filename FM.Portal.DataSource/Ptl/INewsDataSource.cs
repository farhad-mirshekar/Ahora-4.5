using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
    public interface INewsDataSource : IDataSource
    {
        Result<News> Insert(News model);
        Result<News> Update(News model);
        DataTable List(NewsListVM listVM);
        Result<News> Get(Guid ID);
        Result<int> Delete(Guid ID);

    }
}

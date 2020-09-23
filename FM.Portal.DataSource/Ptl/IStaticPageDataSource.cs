using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IStaticPageDataSource:IDataSource
    {
        Result<StaticPage> Update(StaticPage model);
        Result<StaticPage> Get(Guid ID);
        Result<StaticPage> Get(string TrackingCode);
        DataTable List(StaticPageListVM listVM);
        Result Delete(Guid ID);
    }
}

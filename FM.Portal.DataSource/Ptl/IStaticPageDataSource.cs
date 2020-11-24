using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IStaticPageDataSource:IDataSource
    {
        Result<StaticPage> Update(StaticPage model);
        Result<StaticPage> Get(Guid ID);
        DataTable List(StaticPageListVM listVM);
        Result Delete(Guid ID);
    }
}

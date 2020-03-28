using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IRoleDataSource : IDataSource
    {
        Result<Role> Insert(Role model);
        Result<Role> Update(Role model);
        Result Delete(Guid ID);
        DataTable List();
        Result<Role> Get(Guid id);

    }
}

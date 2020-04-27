using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
    public interface IPagesDataSource : IDataSource
    {
        Result<Pages> Create(Pages model);
        Result<Pages> Update(Pages model);
        Result<Pages> Get(Guid ID);
        DataTable List();
        Result Delete(Guid ID);

    }
}

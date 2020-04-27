using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
    public interface IMenuDataSource : IDataSource
    {
        Result<Menu> Create(Menu model);
        Result<Menu> Update(Menu model);
        Result<Menu> Get(Guid ID);
        Result<Menu> Get(string ParentNode);
        DataTable List();
        DataTable GetChildren(string ParentNode);
        Result Delete(Guid ID);

    }
}

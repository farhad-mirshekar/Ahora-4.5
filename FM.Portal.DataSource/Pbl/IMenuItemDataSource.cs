using FM.Portal.Core;
using FM.Portal.Core.Model;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IMenuItemDataSource:IDataSource
    {
        Result<MenuItem> Insert(MenuItem model);
        Result<MenuItem> Update(MenuItem model);
        Result<MenuItem> Get(Guid ID);
        DataTable List(MenuItemListVM listVM);
        Result Delete(Guid ID);
    }
}

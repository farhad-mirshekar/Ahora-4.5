using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IMenuItemService:IService
    {
        Result<MenuItem> Add(MenuItem model);
        Result<MenuItem> Edit(MenuItem model);
        Result<MenuItem> Get(Guid ID);
        Result<List<MenuItem>> List(MenuItemListVM listVM);
        Result Delete(Guid ID);
    }
}

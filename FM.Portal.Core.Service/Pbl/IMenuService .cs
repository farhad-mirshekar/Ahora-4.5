using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IMenuService : IService
    {
        Result<Menu> Add(Menu model);
        Result<Menu> Edit(Menu model);
        Result<Menu> Get(Guid ID);
        Result<List<Menu>> List(MenuListVM listVM);
        Result Delete(Guid ID);
    }
}

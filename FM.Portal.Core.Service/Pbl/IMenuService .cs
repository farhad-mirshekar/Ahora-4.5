using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IMenuService : IService
    {
        Result<Menu> Add(Menu model);
        Result<Menu> Edit(Menu model);
        Result<Menu> Get(Guid ID);
        Result<Menu> Get(string ParentNode);
        Result<List<Menu>> List();
        Result<List<MenuVM>> GetMenuForWeb(string Node);
        Result.Result Delete(Guid ID);
    }
}

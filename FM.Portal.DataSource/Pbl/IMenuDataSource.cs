﻿using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
    public interface IMenuDataSource : IDataSource
    {
        Result<Menu> Insert(Menu model);
        Result<Menu> Update(Menu model);
        Result<Menu> Get(Guid ID);
        DataTable List(MenuListVM listVM);
        Result Delete(Guid ID);

    }
}

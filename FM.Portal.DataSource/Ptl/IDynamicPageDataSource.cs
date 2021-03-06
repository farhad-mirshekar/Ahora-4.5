﻿using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IDynamicPageDataSource:IDataSource
    {
        Result<DynamicPage> Insert(DynamicPage model);
        Result<DynamicPage> Update(DynamicPage model);
        Result<DynamicPage> Get(Guid ID);
        DataTable List(DynamicPageListVM listVM);
        Result Delete(Guid ID);
    }
}

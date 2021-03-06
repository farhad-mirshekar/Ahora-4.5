﻿using FM.Portal.Core.Model;
using FM.Portal.Core;

namespace FM.Portal.DataSource
{
   public interface IOrderDataSource:IDataSource
    {
        Result Insert(Order model);
        Result<Order> Get(GetOrderVM model);
        Result<Order> Update(Order model);
    }
}

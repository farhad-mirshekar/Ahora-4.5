﻿using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;

namespace FM.Portal.Core.Service
{
    public interface IOrderService : IService
    {
        Result.Result Add(Order model,OrderDetail detail);
        Result<Order> Get(Guid ID);
    }
}

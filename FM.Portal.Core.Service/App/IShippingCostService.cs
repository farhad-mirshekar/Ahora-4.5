﻿using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IShippingCostService : IService
    {
        Result<ShippingCost> Add(ShippingCost model);
        Result<ShippingCost> Edit(ShippingCost model);
        Result<List<ShippingCost>> List(ShippingCostListVM listVM);
        Result<ShippingCost> Get(Guid ID);
        Result.Result Delete(Guid ID);
    }
}

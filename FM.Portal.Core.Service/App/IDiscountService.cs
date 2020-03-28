using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IDiscountService : IService
    {
        Result<Discount> Add(Discount model);
        Result<Discount> Edit(Discount model);
        Result<List<Discount>> List();
        Result<Discount> Get(Guid ID);
    }
}

using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IProductMapAttributeService:IService
    {
        Result<ProductMapAttribute> Add(ProductMapAttribute model);
        Result<ProductMapAttribute> Edit(ProductMapAttribute model);
        Result<List<ListAttributeForProductVM>> List(Guid ProductID);
        Result<ProductMapAttribute> Get(Guid ID);
        Result.Result Delete(Guid ID);
    }
}

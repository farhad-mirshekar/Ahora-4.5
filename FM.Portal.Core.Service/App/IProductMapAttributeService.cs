using FM.Portal.Core.Model;
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
        Result Delete(Guid ID);
    }
}

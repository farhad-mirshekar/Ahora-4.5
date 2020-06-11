using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IProductTypeService:IService
    {
        Result<ProductType> Add(ProductType model);
        Result<ProductType> Edit(ProductType model);
        Result<List<ProductType>> List(ProductTypeListVM listVM);
        Result<ProductType> Get(Guid ID);
        Result.Result Delete(Guid ID);
    }
}

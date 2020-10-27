using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IProductAttributeService:IService
    {
        Result<ProductAttribute> Add(ProductAttribute model);
        Result<ProductAttribute> Edit(ProductAttribute model);
        Result<ProductAttribute> Get(Guid ID);
        Result<List<ProductAttribute>> List(ProductAttributeListVM listVM);
    }
}

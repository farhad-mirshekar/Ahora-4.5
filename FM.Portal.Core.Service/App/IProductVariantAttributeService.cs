﻿using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IProductVariantAttributeService:IService
    {
        Result<ProductVariantAttribute> Add(ProductVariantAttribute model);
        Result<ProductVariantAttribute> Edit(ProductVariantAttribute model);
        Result<List<ProductVariantAttribute>> List(Guid ProductVariantAttributeID);
        Result<ProductVariantAttribute> Get(Guid ID);
        Result Delete(Guid ID);
    }
}

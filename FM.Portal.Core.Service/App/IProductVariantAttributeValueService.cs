using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
    public interface IProductVariantAttributeValueService : IService
    {
        Result<ProductVariantAttributeValue> Add(ProductVariantAttributeValue model);
        Result<ProductVariantAttributeValue> Edit(ProductVariantAttributeValue model);
        Result<List<ProductVariantAttributeValue>> List(Guid ProductVariantAttributeID);
        Result<ProductVariantAttributeValue> Get(Guid ID);
        Result Delete(Guid ID);
    }
}

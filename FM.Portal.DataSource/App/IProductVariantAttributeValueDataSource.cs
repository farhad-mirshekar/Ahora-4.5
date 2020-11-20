using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IProductVariantAttributeValueDataSource : IDataSource
    {
        Result<ProductVariantAttributeValue> Insert(ProductVariantAttributeValue model);
        Result<ProductVariantAttributeValue> Update(ProductVariantAttributeValue model);
        DataTable List(Guid ProductVariantAttributeID);
        Result<ProductVariantAttributeValue> Get(Guid ID);
        Result Delete(Guid ID);
    }
}

using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IProductVariantAttributeDataSource : IDataSource
    {
        Result<ProductVariantAttribute> Insert(ProductVariantAttribute model);
        Result<ProductVariantAttribute> Update(ProductVariantAttribute model);
        DataTable List(Guid ProductVariantAttributeID);
        Result<ProductVariantAttribute> Get(Guid ID);
        Result Delete(Guid ID);
    }
}

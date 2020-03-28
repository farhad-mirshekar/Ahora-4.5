using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
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
    }
}

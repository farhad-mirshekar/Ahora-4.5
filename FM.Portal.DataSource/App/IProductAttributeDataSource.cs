using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IProductAttributeDataSource : IDataSource
    {
        Result<ProductAttribute> Insert(ProductAttribute model);
        Result<ProductAttribute> Update(ProductAttribute model);
        Result<ProductAttribute> Get(Guid ID);
        DataTable List(ProductAttributeListVM listVM);
    }
}

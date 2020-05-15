using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IProductMapAttributeDataSource : IDataSource
    {
        Result<ProductMapAttribute> Insert(ProductMapAttribute model);
        Result<ProductMapAttribute> Update(ProductMapAttribute model);
        DataTable List(Guid ProductID);
        Result<ProductMapAttribute> Get(Guid ID);
        Result Delete(Guid ID);
    }
}

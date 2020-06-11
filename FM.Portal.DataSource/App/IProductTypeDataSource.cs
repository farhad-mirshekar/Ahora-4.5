using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IProductTypeDataSource:IDataSource
    {
        Result<ProductType> Insert(ProductType model);
        Result<ProductType> Update(ProductType model);
        DataTable List(ProductTypeListVM listVM);
        Result<ProductType> Get(Guid ID);
        Result Delete(Guid ID);
    }
}

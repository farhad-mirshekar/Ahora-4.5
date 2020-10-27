using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IRelatedProductDataSource:IDataSource
    {
        Result<RelatedProduct> Insert(RelatedProduct model);
        Result<RelatedProduct> Update(RelatedProduct model);
        DataTable List(RelatedProductListVM listVM);
        Result<RelatedProduct> Get(Guid ID);
        Result Delete(Guid ID);
    }
}

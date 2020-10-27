using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IProductDataSource : IDataSource
    {
        Result<Product> Insert(Product model);
        Result<Product> Update(Product model);
        DataTable List(ProductListVM listVM);
        DataTable List(Guid CategoryID);
        Result<Product> Get(Guid? ID , string TrackingCode);
        DataTable ListAttributeForProduct(Guid ProductID);
        DataTable ListProductShowOnHomePage(int Count);
        DataTable ListProductVarientAttribute(Guid AttributeID);
    }
}

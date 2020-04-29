using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IShoppingCartItemDataSource : IDataSource
    {
        DataTable Insert(ShoppingCartItem model);
        DataTable Update(ShoppingCartItem model);
        DataTable List(Guid ShoppingID);
        DataTable Delete(DeleteCartItemVM model);
        Result Delete(Guid ShoppingID);
        Result<ShoppingCartItem> Get(Guid ShoppingID, Guid ProductID);
    }
}

using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IShoppingCartItemService : IService
    {
        Result<List<ShoppingCartItem>> Add(ShoppingCartItem model);
        Result<List<ShoppingCartItem>> Edit(ShoppingCartItem model);
        Result<List<ShoppingCartItem>> List(Guid ShoppingID);
        Result<List<ShoppingCartItem>> Delete(DeleteCartItemVM model);
        Result<ShoppingCartItem> Get(Guid ShoppingID , Guid ProductID);
    }
}

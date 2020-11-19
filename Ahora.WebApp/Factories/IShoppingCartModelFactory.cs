using Ahora.WebApp.Models;
using Ahora.WebApp.Models.App;
using System;
using System.Collections.Generic;

namespace Ahora.WebApp.Factories
{
   public interface IShoppingCartModelFactory
    {
        ShoppingCartItemListModel CartDetail();
        JsonResultModel QuantityPlus(Guid ProductID);
        JsonResultModel QuantityMinus(Guid ProductID);
        JsonResultModel DeleteShoppingCartItem(Guid ProductID);
    }
}

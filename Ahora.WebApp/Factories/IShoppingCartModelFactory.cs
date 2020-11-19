using Ahora.WebApp.Models;
using Ahora.WebApp.Models.App;
using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ahora.WebApp.Factories
{
   public interface IShoppingCartModelFactory
    {
        ShoppingCartItemListModel CartDetail();
        JsonResultModel QuantityPlus(Guid ProductID);
        JsonResultModel QuantityMinus(Guid ProductID);
        JsonResultModel DeleteShoppingCartItem(Guid ProductID);
        ShoppingCartItemListModel ShoppingCartDetail();
        Task<JsonResultModel> Payment(UserAddress model);
    }
}

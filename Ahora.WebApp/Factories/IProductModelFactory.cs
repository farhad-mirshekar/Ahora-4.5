using Ahora.WebApp.Models;
using Ahora.WebApp.Models.App;
using System;
using System.Web.Mvc;

namespace Ahora.WebApp.Factories
{
   public interface IProductModelFactory
    {
        ProductModel GetProduct(Guid ID);
        JsonResultModel AddToCartWithAttribute(ProductModel product, FormCollection collection);
        JsonResultModel AddToCartWithNotAttribute(ProductModel product);
    }
}

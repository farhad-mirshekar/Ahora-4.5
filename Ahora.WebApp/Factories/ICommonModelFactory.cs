using Ahora.WebApp.Models.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ahora.WebApp.Factories
{
    public interface ICommonModelFactory
    {
        List<ProductOverviewModel> TrendingProduct(Guid? LanguageID);
        List<ProductOverviewModel> HasDiscountProduct(Guid? LanguageID);
    }
}
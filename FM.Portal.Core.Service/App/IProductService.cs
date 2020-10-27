using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IProductService : IService
    {
        Result<Product> Add(Product model);
        Result<Product> Edit(Product model);
        Result<List<Product>> List(ProductListVM listVM);
        Result<List<ListProductByCategoryIDVM>> List(Guid CategoryID);
        Result<Product> Get(Guid ID);
        Result<Product> Get(string TrackingCode);
        Result<List<ListProductShowOnHomePageVM>> ListProductShowOnHomePage(int Count);
        Result<List<ListAttributeForSelectCustomerVM>> SelectAttributeForCustomer(Guid ProductID);
    }
}

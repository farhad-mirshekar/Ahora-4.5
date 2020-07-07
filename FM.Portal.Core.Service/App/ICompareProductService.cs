using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface ICompareProductService:IService
    {
        void ClearCompareProducts();
        Result<List<Product>> GetComparedProducts();
        void RemoveProductFromCompareList(Guid ProductID);
        void AddProductToCompareList(Guid ProductID);
    }
}

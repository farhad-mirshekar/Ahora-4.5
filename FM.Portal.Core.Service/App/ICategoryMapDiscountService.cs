using FM.Portal.Core.Model;
using System;

namespace FM.Portal.Core.Service
{
   public interface ICategoryMapDiscountService:IService
    {
        Result<CategoryMapDiscount> Add(CategoryMapDiscount model);
        Result<CategoryMapDiscount> Get(Guid? CategoryID , Guid? DiscountID);
    }
}

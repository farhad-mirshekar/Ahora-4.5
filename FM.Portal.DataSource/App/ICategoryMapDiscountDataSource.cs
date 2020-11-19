using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;

namespace FM.Portal.DataSource
{
   public interface ICategoryMapDiscountDataSource : IDataSource
    {
        Result<CategoryMapDiscount> Insert(CategoryMapDiscount model);
        Result<CategoryMapDiscount> Get(Guid? CategoryID, Guid? DiscountID);

    }
}

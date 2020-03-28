using FM.Portal.Core.Model;
using FM.Portal.Core.Result;

namespace FM.Portal.DataSource
{
   public interface ICategoryMapDiscountDataSource : IDataSource
    {
        Result<CategoryMapDiscount> Create(CategoryMapDiscount model);

    }
}

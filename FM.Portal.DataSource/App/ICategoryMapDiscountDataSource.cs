using FM.Portal.Core.Model;
using FM.Portal.Core;

namespace FM.Portal.DataSource
{
   public interface ICategoryMapDiscountDataSource : IDataSource
    {
        Result<CategoryMapDiscount> Create(CategoryMapDiscount model);

    }
}

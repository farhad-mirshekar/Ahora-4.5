using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
namespace FM.Portal.Core.Service
{
   public interface ICategoryMapDiscountService:IService
    {
        Result<CategoryMapDiscount> Add(CategoryMapDiscount model);
    }
}

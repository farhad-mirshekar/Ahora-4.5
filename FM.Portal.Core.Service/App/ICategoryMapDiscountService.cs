using FM.Portal.Core.Model;
namespace FM.Portal.Core.Service
{
   public interface ICategoryMapDiscountService:IService
    {
        Result<CategoryMapDiscount> Add(CategoryMapDiscount model);
    }
}

using System;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;

namespace FM.Portal.Domain
{
    public class CategoryMapDiscountService : ICategoryMapDiscountService
    {
        private readonly ICategoryMapDiscountDataSource _dataSource;
        public CategoryMapDiscountService(ICategoryMapDiscountDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<CategoryMapDiscount> Add(CategoryMapDiscount model)
        => _dataSource.Create(model);
    }
}

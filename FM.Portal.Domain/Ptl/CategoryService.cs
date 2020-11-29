using FM.Portal.Core.Common;
using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core;
using FM.Portal.Core.Service.Ptl;
using FM.Portal.DataSource.Ptl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FM.Portal.Domain.Ptl
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDataSource _dataSource;
        public CategoryService(ICategoryDataSource dataSource) { _dataSource = dataSource; }
        public Result<Category> Add(Category model)
        {
            var validationResult = ValidationModel(model);
            if (!validationResult.Success)
                return Result<Category>.Failure(message: validationResult.Message);

            model.ID = Guid.NewGuid();
           return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<Category> Edit(Category model)
        {
            var validationResult = ValidationModel(model);
            if (!validationResult.Success)
                return Result<Category>.Failure(message: validationResult.Message);
            return _dataSource.Update(model);
        }

        public Result<Category> Get(Guid ID)
            => _dataSource.Get(ID);
        public Result<List<GetCountCategoryVM>> GetCountCategory()
        {
            var table = ConvertDataTableToList.BindList<GetCountCategoryVM>(_dataSource.GetCountCategory());
            if (table.Count > 0 || table.Count == 0)
                return Result<List<GetCountCategoryVM>>.Successful(data: table);
            return Result<List<GetCountCategoryVM>>.Failure();
        }

        public Result<List<Category>> List()
        {
            var table = ConvertDataTableToList.BindList<Category>(_dataSource.List());
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Category>>.Successful(data: table);
            return Result<List<Category>>.Failure();
        }

        private Result ValidationModel(Category category)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(category.Title))
                errors.Add("نام دسته بندی را وارد نمایید");

            if (errors.Any())
                return Result.Failure(message: string.Join("&&", errors));

            return Result.Successful();
        }
    }
}

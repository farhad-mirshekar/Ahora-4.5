using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using FM.Portal.Core.Result;

namespace FM.Portal.Domain
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDataSource _dataSource;
        public CategoryService(ICategoryDataSource dataSource) { _dataSource = dataSource; }
        public Result<Category> Add(Category model)
            => _dataSource.Insert(model);

        public Result<Category> Edit(Category model)
            => _dataSource.Update(model);

        public Result<Category> Get(Guid ID)
            => _dataSource.Get(ID);

        public Result<Category> GetByParent(Guid ID)
            => _dataSource.GetByParent(ID);

        public Result<List<Category>> List()
        {
            var table =ConvertDataTableToList.BindList<Category>(_dataSource.List());
            if (table.Count > 0)
                return Result<List<Category>>.Successful(data: table);
            return Result<List<Category>>.Failure();
        }
    }
}

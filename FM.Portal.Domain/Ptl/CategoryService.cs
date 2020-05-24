using FM.Portal.Core.Common;
using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service.Ptl;
using FM.Portal.DataSource.Ptl;
using System;
using System.Collections.Generic;

namespace FM.Portal.Domain.Ptl
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

        public Result<List<Category>> ListByNode(string Node)
        {
            var table = ConvertDataTableToList.BindList<Category>(_dataSource.ListByNode(Node));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Category>>.Successful(data: table);
            return Result<List<Category>>.Failure();
        }

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
    }
}

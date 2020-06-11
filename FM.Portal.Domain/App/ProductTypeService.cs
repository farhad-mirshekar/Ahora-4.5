using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FM.Portal.Domain
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeDataSource _dataSource;
        public ProductTypeService(IProductTypeDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<ProductType> Add(ProductType model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<ProductType>.Failure(message: validateResult.Message);
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<ProductType> Edit(ProductType model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<ProductType>.Failure(message: validateResult.Message);

            return _dataSource.Update(model);
        }

        public Result<ProductType> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<ProductType>> List(ProductTypeListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<ProductType>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<ProductType>>.Successful(data: table);
            return Result<List<ProductType>>.Failure();
        }

        private Result ValidateModel(ProductType model)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(model.Description))
                errors.Add("توضیحات کوتاه را وارد نمایید");
            if (string.IsNullOrEmpty(model.Name))
                errors.Add("نوع محصول وارد نمایید");
            if (errors.Any())
                return Result.Failure(message: string.Join("&&", errors));
            return Result.Successful();
        }
    }
}

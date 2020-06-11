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
    public class ShippingCostService : IShippingCostService
    {
        private readonly IShippingCostDataSource _dataSource;
        public ShippingCostService(IShippingCostDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<ShippingCost> Add(ShippingCost model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<ShippingCost>.Failure(message: validateResult.Message);
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<ShippingCost> Edit(ShippingCost model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<ShippingCost>.Failure(message: validateResult.Message);

            return _dataSource.Update(model);
        }

        public Result<ShippingCost> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<ShippingCost>> List(ShippingCostListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<ShippingCost>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<ShippingCost>>.Successful(data: table);
            return Result<List<ShippingCost>>.Failure();
        }

        private Result ValidateModel(ShippingCost model)
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

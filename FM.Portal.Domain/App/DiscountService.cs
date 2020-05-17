using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using FM.Portal.Core.Result;

namespace FM.Portal.Domain
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountDataSource _dataSource;
        public DiscountService(IDiscountDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<Discount> Add(Discount model)
        {
            var validation = ValidationModel(model);
            if (!validation.Success)
                return Result<Discount>.Failure(message: validation.Message);
            return _dataSource.Insert(model);
        }

        public Result<Discount> Edit(Discount model)
        {
            var validation = ValidationModel(model);
            if (!validation.Success)
                return Result<Discount>.Failure(message: validation.Message);
            return _dataSource.Update(model);
        }

        public Result<Discount> Get(Guid ID)
        {
           return _dataSource.Get(ID);
        }

        public Result<List<Discount>> List()
        {
            var table = ConvertDataTableToList.BindList<Discount>(_dataSource.List());
            if (table.Count > 0)
                return Result<List<Discount>>.Successful(data: table);
            return Result<List<Discount>>.Failure();
        }

        private Result ValidationModel(Discount model)
        {
            if (model.Name == null || model.Name == "")
                return Result.Failure(message: "نام تخفیف را وارد نمایید");
            if(model.DiscountType == DiscountType.درصدی)
            {
                if(model.DiscountAmount == 0)
                    return Result.Failure(message: "درصد تخفیف وارد شود");
            }
            if (model.DiscountType == DiscountType.مبلغی)
            {
                if (model.DiscountAmount == 0)
                    return Result.Failure(message: "مبلغ تخفیف وارد شود");
            }
            return Result.Successful();
        }
    }
}

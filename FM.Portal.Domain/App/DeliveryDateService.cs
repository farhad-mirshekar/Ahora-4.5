using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FM.Portal.Domain
{
   public class DeliveryDateService: IDeliveryDateService
    {
        private readonly IDeliveryDateDataSource _dataSource;
        public DeliveryDateService(IDeliveryDateDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<DeliveryDate> Add(DeliveryDate model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<DeliveryDate>.Failure(message: validateResult.Message);
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<DeliveryDate> Edit(DeliveryDate model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<DeliveryDate>.Failure(message: validateResult.Message);

            return _dataSource.Update(model);
        }

        public Result<DeliveryDate> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<DeliveryDate>> List(DeliveryDateListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<DeliveryDate>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<DeliveryDate>>.Successful(data: table);
            return Result<List<DeliveryDate>>.Failure();
        }

        private Result ValidateModel(DeliveryDate model)
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

using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;

namespace FM.Portal.Domain
{
    public class SalesService : ISalesService
    {
        private readonly ISalesDataSource _dataSource;
        public SalesService(ISalesDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<Sales> Add(Sales model)
        {
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Confirm(FlowConfirmVM confirmVM)
        {
            try
            {
                var salesResult = Get(confirmVM.DocumentID);
                if (!salesResult.Success)
                    return Result.Failure(message: "خطا در بازیابی اطلاعات");
                var sales = salesResult.Data;

                switch (sales.LastDocState)
                {
                    case SalesDocState.بررسی_و_ارجاع_به_واحد_مالی:

                        break;
                    case SalesDocState.بررسی_و_ارجاع_به_واحد_انبار:

                        break;

                    case SalesDocState.آماده_بسته_بندی:
                        break;

                    case SalesDocState.ارسال_محصول:

                        break;
                }

                return null;
            }
            catch(Exception e) { throw; }
        }

        public Result<Sales> Edit(Sales model)
        => _dataSource.Update(model);

        public Result<Sales> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<Sales>> List(SalesListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<Sales>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Sales>>.Successful(data: table);
            return Result<List<Sales>>.Failure();
        }
    }
}

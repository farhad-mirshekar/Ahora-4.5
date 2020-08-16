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

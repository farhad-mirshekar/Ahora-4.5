using FM.Portal.Core;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;

namespace FM.Portal.Domain
{
    public class ActivityLogTypeService : IActivityLogTypeService
    {
        private readonly IActivityLogTypeDataSource _dataSource;
        public ActivityLogTypeService(IActivityLogTypeDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<ActivityLogType> Add(ActivityLogType model)
        {
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);

        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<ActivityLogType> Edit(ActivityLogType model)
        => _dataSource.Update(model);

        public Result<ActivityLogType> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<ActivityLogType>> List(ActivityLogTypeListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<ActivityLogType>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<ActivityLogType>>.Successful(data: table);
            return Result<List<ActivityLogType>>.Failure();
        }
    }
}

using FM.Portal.Core;
using FM.Portal.Core.Model;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
    public interface IActivityLogTypeDataSource : IDataSource
    {
        Result<ActivityLogType> Insert(ActivityLogType model);
        Result<ActivityLogType> Update(ActivityLogType model);
        Result<ActivityLogType> Get(Guid ID);
        Result Delete(Guid ID);
        DataTable List(ActivityLogTypeListVM listVM);
    }
}

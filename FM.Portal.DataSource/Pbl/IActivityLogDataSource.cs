using FM.Portal.Core;
using FM.Portal.Core.Model;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IActivityLogDataSource : IDataSource
    {
        Result<ActivityLog> Insert(ActivityLog model);
        Result<ActivityLog> Update(ActivityLog model);
        Result<ActivityLog> Get(Guid ID);
        Result Delete(Guid ID);
        DataTable List(ActivityLogListVM listVM);
    }
}

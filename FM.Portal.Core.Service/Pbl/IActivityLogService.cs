using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IActivityLogService:IService
    {
        Result<ActivityLog> Add(ActivityLog model);
        Result<ActivityLog> Edit(ActivityLog model);
        Result<ActivityLog> Get(Guid ID);
        Result Delete(Guid ID);
        Result<List<ActivityLog>> List(ActivityLogListVM listVM);
    }
}
